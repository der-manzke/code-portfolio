namespace dnd_helper.Schema
{
    /// <summary>
    /// Structure of JSON file from 5e.tools
    /// </summary>
    public class LootJson
    {
        public string? síteVersion { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? dateTimeGenerated { get; set; }

        public Currency? currency { get; set; }

        public EntityInfo[]? entityInfos { get; set; }

        /// <summary>
        /// Currency structure
        /// </summary>
        public partial class Currency { 
            public string? cp { get; set; }
            public string? sp { get; set; }
            public string? gp { get; set; }
            public string? pp { get; set; }
        }

        /// <summary>
        /// Item structure
        /// </summary>
        public partial class  EntityInfo
        {
            public string? page {  get; set; }

            public Entity? entity { get; set; }

            public Options? options { get; set; }
            public partial class Entity
            {
                public string? name { get; set; }
                public int? level { get; set; }
                public string? type { get; set; }
                public int? value { get; set; }
                public string? _typeHtml { get; set; }
            }

            public partial class Options { 
                public int? quantity { get; set; }
            }
        }
    }
}
