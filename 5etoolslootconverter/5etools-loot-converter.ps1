[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.VisualBasic") | Out-Null
function Open-File([string] $initialDirectory) {

    [System.Reflection.Assembly]::LoadWithPartialName("System.windows.forms") | Out-Null

    $OpenFileDialog = New-Object System.Windows.Forms.OpenFileDialog
    $OpenFileDialog.initialDirectory = $initialDirectory
    $OpenFileDialog.filter = "5e.tools Loot File (*.json)|*.json"
    $OpenFileDialog.ShowDialog() |  Out-Null

    return $OpenFileDialog.filename
} 

function Get-Currencies($currencyList) {
    $pp = $currencyList.pp
    $gp = $currencyList.gp
    $sp = $currencyList.sp
    $cp = $currencyList.cp

    $output += "PP: $pp`n"
    $output += "GP: $gp`n"
    $output += "SP: $sp`n"
    $output += "CP: $cp`n"
 
    return $output
}

function Get-Items($itemList) {
    $potions = ""
    $scrolls = ""
    $spells = ""
    $melee = ""
    $ranged = ""
    $rod = ""
    $staff = ""
    $wand = ""
    $ammunition = ""
    $instruments = ""
    $wUnsorted
    $hArmor = ""
    $mArmor = ""
    $lArmor = ""
    $shield = ""
    $aUnsorted = ""
    $rings = ""
    $gems = ""
    $treasure = ""
    $wItems = @()
    $misc = ""

    foreach ($item in $itemList) {

        $quantity = $item.options.quantity
        if (!$quantity) { $quantity = 1 }

        $name = $item.entity.name

        $value = ""
        if ($item.entity.value) {
            $value = "(" + [string]($item.entity.value / 100) + " GP)"
            if ($item.entity._fValue -eq 0) { $value = "" }
        }

        $type = if ($item.entity.type) { $item.entity.type } else { $item.entity._typeHtml }
        switch -Wildcard ($type) {
            "potion" {
                $potions += "$name $value`n"
            }
            "P" {
                $potions += "$name $value`n"
            }
            "SC" {
                $scrolls += "$name`n"
            }
            "scroll" {
                $scrolls += "$name`n"
            }
            "staff*" {
                $staff += "$name`n"
            }
            "RG" {
                $rings += "$name`n"
            }
            "INS" {
                $instruments += "$name (Instrument)`n"
            }
            "M" {
                $melee += "$name`n"
            }
            "R" {
                $ranged += "$name`n"
            }
            "S" {
                $shield += "$name`n"
            }
            "weapon" {
                $wUnsorted += "$name`n"
            }
            "WD" {
                $wand += "$name`n"
            }
            "wand" {
                $wand += "$name`n"
            }
            "A" {
                $ammunition += "$name`n"
            }
            "AF" {
                $ammunition += "$name`n"
            }           
            "ammunition" {
                $ammunition += "$name`n"
            }
            "RD" {
                $rod += "$name`n"
            }
            "rod" {
                $rod += "$name`n"
            }
            "HA" {
                $hArmor += "$name (heavy)`n"
            }
            "heavy armor" {
                $hArmor += "$name (heavy)`n"
            }
            "MA" {
                $mArmor += "$name (medium)`n"
            }
            "medium armor" {
                $mArmor += "$name (medium)`n"
            }
            "LA" {
                $lArmor += "$name (Light)`n"
            }
            "light armor" {
                $lArmor += "$name (light)`n"
            }
            "armor*" {
                $aUnsorted += "$name`n"
            }
            "`$" {
                if (!$item.entity.entries) {
                    $treasure += "$quantity`x $name $value`n"
                }
                else {
                    $gems += "$quantity`x $name $value`n"
                }
            }
            "treasure" {
                if (!$item.entity.entries) {
                    $treasure += "$quantity`x $name $value`n"
                }
                else {
                    $gems += "$quantity`x $name $value`n"
                }
            }
            "OTH" {
                $misc += "- $name`n"
            }
            "wondrous item*" {
                $wItems += $name
            }
        }

        if ($item.page -eq "spells.html") {
            $level = $item.entity.level
            $ending = "th"
            if ($level -eq 1) { $ending = "st" } elseif ($level -eq 2) { $ending = "nd" } elseif ($level -eq 3) { $ending = "rd" }
            $spells += "$level$ending Level Spell Scroll: $name`n"
        }
    }

    $wItems = $wItems | Sort-Object
    $wItemsOutput = ""
    foreach ($item in $wItems) {
        $wItemsOutput += "$item`r`n"
    }
    $output = ""
    if ($gems) { $output += "**Gems:**`n$gems`n" }
    if ($treasure) { $output += "**Treasure:**`n$treasure`n" }
    if ($potions) { $output += "**Potions:**`n$potions`n" }
    if ($spells -Or $scrolls) { $output += "**Scrolls:**`n$scrolls$spells`n" }
    if ($melee -Or $ranged -Or $rod -Or $wUnsorted) { $output += "**Weapons**`n$melee$ranged$rod$wUnsorted`n" }
    if ($hArmor -Or $mArmor -Or $lArmor -Or $aUnsorted) { $output += "**Armor**`n$hArmor$mArmor$lArmor$aUnsorted`n" }
    if ($rings -Or $wand -Or $staff) { $output += "**Magic Items**`n" }
    if ($rings) { $output += "__Rings__`n$rings`n" }
    if ($wand) { $output += "__Wands__`n$wand`n" }
    if ($staff) { $output += "__Staffs__`n$staff`n" }
    if ($wItemsOutput) { $output += "**Wonderous Items:**`n$wItemsOutput`n" }
    if ($misc) { $output += "**Other Items:**`n$misc`n" }

    return $output
}

$OpenFile = Open-File $env:USERPROFILE\Downloads 

trap [System.SystemException] {
    Set-Clipboard -Value $_
    [System.Windows.Forms.MessageBox]::Show("COUTNERSPELL!1!11! Der ganze Fehler ist in deiner Zwischenablage. Fehler " + $_.ScriptStackTrace, 'ERROR')
    break
}

if ($OpenFile -ne "") {
    $content = Get-Content -Raw $OpenFile | ConvertFrom-Json

    $currency = ""
    $curText = ""
    $itemText = ""

    if ($content.currency) {
        $currency = Get-Currencies($content.currency)
        $curText = "__**Currencies:**__`r$currency`n" 
    }
    $items = Get-Items($content.entityInfos)
    $itemText = "__**Items:**__`r" + $items[1]

    $output = $curText + $itemText.TrimEnd()
    
    Set-Clipboard -Value $output
    [Microsoft.VisualBasic.Interaction]::MsgBox("Loot in deiner Zwischenablage!", "OKOnly,SystemModal", "Dimension-Door-Action!") | Out-Null
}
