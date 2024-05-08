(function(require){
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            waitSeconds: 0,
            map: {
                '*': {
                    'ko': 'knockoutjs/knockout',
                    'knockout': 'knockoutjs/knockout',
                    'mageUtils': 'mage/utils/main',
                    'rjsResolver': 'mage/requirejs/resolver'
                }
            },
            shim: {
                'jquery/jquery-migrate': ['jquery'],
                'jquery/jstree/jquery.hotkeys': ['jquery'],
                'jquery/hover-intent': ['jquery'],
                'mage/adminhtml/backup': ['prototype'],
                'mage/captcha': ['prototype'],
                'mage/new-gallery': ['jquery'],
                'mage/webapi': ['jquery'],
                'jquery/ui': ['jquery'],
                'MutationObserver': ['es6-collections'],
                'matchMedia': {
                    'exports': 'mediaCheck'
                },
                'magnifier/magnifier': ['jquery'],
                'jquery.bootstrap': ['jquery']
            },
            paths: {
                'jquery/validate': 'jquery/jquery.validate',
                'jquery/hover-intent': 'jquery/jquery.hoverIntent',
                'jquery/file-uploader': 'jquery/fileUploader/jquery.fileuploader',
                'prototype': 'legacy-build.min',
                'jquery/jquery-storageapi': 'jquery/jquery.storageapi.min',
                'text': 'mage/requirejs/text',
                'domReady': 'requirejs/domReady',
                'spectrum': 'jquery/spectrum/spectrum',
                'tinycolor': 'jquery/spectrum/tinycolor',
                'jquery-ui-modules': 'jquery/ui-modules',
                'jquery.bootstrap': 'lib/bootstrap-5.3.2/js/bootstrap.bundle.min.js'
            },
            deps: [
                'jquery/jquery-migrate'
            ],
            config: {
                mixins: {
                    'jquery/jstree/jquery.jstree': {
                        'mage/backend/jstree-mixin': true
                    },
                    'jquery': {
                        'jquery/patches/jquery': true
                    }
                },
                text: {
                    'headers': {
                        'X-Requested-With': 'XMLHttpRequest'
                    }
                }
            }
        };

        require(['jquery'], function ($) {
            'use strict';

            $.noConflict();
        });

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    'rowBuilder':             'Magento_Theme/js/row-builder',
                    'toggleAdvanced':         'mage/toggle',
                    'translateInline':        'mage/translate-inline',
                    'sticky':                 'mage/sticky',
                    'tabs':                   'mage/tabs',
                    'zoom':                   'mage/zoom',
                    'collapsible':            'mage/collapsible',
                    'dropdownDialog':         'mage/dropdown',
                    'dropdown':               'mage/dropdowns',
                    'accordion':              'mage/accordion',
                    'loader':                 'mage/loader',
                    'tooltip':                'mage/tooltip',
                    'deletableItem':          'mage/deletable-item',
                    'itemTable':              'mage/item-table',
                    'fieldsetControls':       'mage/fieldset-controls',
                    'fieldsetResetControl':   'mage/fieldset-controls',
                    'redirectUrl':            'mage/redirect-url',
                    'loaderAjax':             'mage/loader',
                    'menu':                   'mage/menu',
                    'popupWindow':            'mage/popup-window',
                    'validation':             'mage/validation/validation',
                    'breadcrumbs':            'Magento_Theme/js/view/breadcrumbs',
                    'jquery/ui':              'jquery/compat',
                    'cookieStatus':           'Magento_Theme/js/cookie-status',
                    'minicart':               'Magento_Checkout/js/view/minicart'
                }
            },
            deps: [
                'jquery/jquery.mobile.custom',
                'mage/common',
                'mage/dataPost',
                'mage/bootstrap'
            ],
            config: {
                mixins: {
                    'Magento_Theme/js/view/breadcrumbs': {
                        'Magento_Theme/js/view/add-home-breadcrumb': true
                    },
                    'jquery/ui-modules/dialog': {
                        'jquery/patches/jquery-ui': true
                    }
                }
            }
        };

        /* eslint-disable max-depth */
        /**
         * Adds polyfills only for browser contexts which prevents bundlers from including them.
         */
        if (typeof window !== 'undefined' && window.document) {
            /**
             * Polyfill localStorage and sessionStorage for browsers that do not support them.
             */
            try {
                if (!window.localStorage || !window.sessionStorage) {
                    throw new Error();
                }

                localStorage.setItem('storage_test', 1);
                localStorage.removeItem('storage_test');
            } catch (e) {
                config.deps.push('mage/polyfill');
            }
        }
        /* eslint-enable max-depth */

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    quickSearch: 'Magento_Search/js/form-mini',
                    'Magento_Search/form-mini': 'Magento_Search/js/form-mini'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    checkoutBalance:    'Magento_Customer/js/checkout-balance',
                    address:            'Magento_Customer/js/address',
                    changeEmailPassword: 'Magento_Customer/js/change-email-password',
                    passwordStrengthIndicator: 'Magento_Customer/js/password-strength-indicator',
                    zxcvbn: 'Magento_Customer/js/zxcvbn',
                    addressValidation: 'Magento_Customer/js/addressValidation',
                    'Magento_Customer/address': 'Magento_Customer/js/address',
                    'Magento_Customer/change-email-password': 'Magento_Customer/js/change-email-password'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    escaper: 'Magento_Security/js/escaper'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    compareList:            'Magento_Catalog/js/list',
                    relatedProducts:        'Magento_Catalog/js/related-products',
                    upsellProducts:         'Magento_Catalog/js/upsell-products',
                    productListToolbarForm: 'Magento_Catalog/js/product/list/toolbar',
                    catalogGallery:         'Magento_Catalog/js/gallery',
                    catalogAddToCart:       'Magento_Catalog/js/catalog-add-to-cart'
                }
            },
            config: {
                mixins: {
                    'Magento_Theme/js/view/breadcrumbs': {
                        'Magento_Catalog/js/product/breadcrumbs': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    priceBox:             'Magento_Catalog/js/price-box',
                    priceOptionDate:      'Magento_Catalog/js/price-option-date',
                    priceOptionFile:      'Magento_Catalog/js/price-option-file',
                    priceOptions:         'Magento_Catalog/js/price-options',
                    priceUtils:           'Magento_Catalog/js/price-utils'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    creditCardType: 'Magento_Payment/js/cc-type',
                    'Magento_Payment/cc-type': 'Magento_Payment/js/cc-type'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    addToCart: 'Magento_Msrp/js/msrp'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    giftMessage:    'Magento_Sales/js/gift-message',
                    ordersReturns:  'Magento_Sales/js/orders-returns',
                    'Magento_Sales/gift-message':    'Magento_Sales/js/gift-message',
                    'Magento_Sales/orders-returns':  'Magento_Sales/js/orders-returns'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    discountCode:           'Magento_Checkout/js/discount-codes',
                    shoppingCart:           'Magento_Checkout/js/shopping-cart',
                    regionUpdater:          'Magento_Checkout/js/region-updater',
                    sidebar:                'Magento_Checkout/js/sidebar',
                    checkoutLoader:         'Magento_Checkout/js/checkout-loader',
                    checkoutData:           'Magento_Checkout/js/checkout-data',
                    proceedToCheckout:      'Magento_Checkout/js/proceed-to-checkout',
                    catalogAddToCart:       'Magento_Catalog/js/catalog-add-to-cart'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    requireCookie: 'Magento_Cookie/js/require-cookie',
                    cookieNotices: 'Magento_Cookie/js/notices'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            paths: {
                'jquery/jquery-storageapi': 'Magento_Cookie/js/jquery.storageapi.extended'
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    downloadable: 'Magento_Downloadable/js/downloadable',
                    'Magento_Downloadable/downloadable': 'Magento_Downloadable/js/downloadable'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    captcha: 'Magento_Captcha/js/captcha',
                    'Magento_Captcha/captcha': 'Magento_Captcha/js/captcha'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    configurable: 'Magento_ConfigurableProduct/js/configurable'
                }
            },
            config: {
                mixins: {
                    'Magento_Catalog/js/catalog-add-to-cart': {
                        'Magento_ConfigurableProduct/js/catalog-add-to-cart-mixin': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    catalogSearch: 'Magento_CatalogSearch/form-mini'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    giftOptions:    'Magento_GiftMessage/js/gift-options',
                    extraOptions:   'Magento_GiftMessage/js/extra-options',
                    'Magento_GiftMessage/gift-options':    'Magento_GiftMessage/js/gift-options',
                    'Magento_GiftMessage/extra-options':   'Magento_GiftMessage/js/extra-options'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            deps: [],
            shim: {
                'chartjs/Chart.min': ['moment'],
                'tiny_mce_4/tinymce.min': {
                    exports: 'tinyMCE'
                }
            },
            paths: {
                'ui/template': 'Magento_Ui/templates'
            },
            map: {
                '*': {
                    uiElement:      'Magento_Ui/js/lib/core/element/element',
                    uiCollection:   'Magento_Ui/js/lib/core/collection',
                    uiComponent:    'Magento_Ui/js/lib/core/collection',
                    uiClass:        'Magento_Ui/js/lib/core/class',
                    uiEvents:       'Magento_Ui/js/lib/core/events',
                    uiRegistry:     'Magento_Ui/js/lib/registry/registry',
                    consoleLogger:  'Magento_Ui/js/lib/logger/console-logger',
                    uiLayout:       'Magento_Ui/js/core/renderer/layout',
                    buttonAdapter:  'Magento_Ui/js/form/button-adapter',
                    chartJs:        'chartjs/Chart.min',
                    tinymce4:       'tiny_mce_4/tinymce.min',
                    wysiwygAdapter: 'mage/adminhtml/wysiwyg/tiny_mce/tinymce4Adapter'
                }
            }
        };

        /**
         * Adds polyfills only for browser contexts which prevents bundlers from including them.
         */
        if (typeof window !== 'undefined' && window.document) {
            /**
             * Polyfill Map and WeakMap for older browsers that do not support them.
             */
            if (typeof Map === 'undefined' || typeof WeakMap === 'undefined') {
                config.deps.push('es6-collections');
            }

            /**
             * Polyfill MutationObserver only for the browsers that do not support it.
             */
            if (typeof MutationObserver === 'undefined') {
                config.deps.push('MutationObserver');
            }

            /**
             * Polyfill FormData object for old browsers that don't have full support for it.
             */
            if (typeof FormData === 'undefined' || typeof FormData.prototype.get === 'undefined') {
                config.deps.push('FormData');
            }
        }

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    pageCache:  'Magento_PageCache/js/page-cache'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    bundleOption:   'Magento_Bundle/bundle',
                    priceBundle:    'Magento_Bundle/js/price-bundle',
                    slide:          'Magento_Bundle/js/slide',
                    productSummary: 'Magento_Bundle/js/product-summary'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    multiShipping: 'Magento_Multishipping/js/multi-shipping',
                    orderOverview: 'Magento_Multishipping/js/overview',
                    payment: 'Magento_Multishipping/js/payment',
                    billingLoader: 'Magento_Checkout/js/checkout-loader',
                    cartUpdate: 'Magento_Checkout/js/action/update-shopping-cart'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    recentlyViewedProducts: 'Magento_Reports/js/recently-viewed'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        var config = {
            config: {
                mixins: {
                    'Magento_Checkout/js/model/quote': {
                        'Magento_InventoryInStorePickupFrontend/js/model/quote-ext': true
                    },
                    'Magento_Checkout/js/view/shipping-information': {
                        'Magento_InventoryInStorePickupFrontend/js/view/shipping-information-ext': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    subscriptionStatusResolver: 'Magento_Newsletter/js/subscription-status-resolver',
                    newsletterSignUp:  'Magento_Newsletter/js/newsletter-sign-up'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            config: {
                mixins: {
                    'Magento_Checkout/js/action/select-payment-method': {
                        'Magento_SalesRule/js/action/select-payment-method-mixin': true
                    },
                    'Magento_Checkout/js/model/shipping-save-processor': {
                        'Magento_SalesRule/js/model/shipping-save-processor-mixin': true
                    },
                    'Magento_Checkout/js/action/place-order': {
                        'Magento_SalesRule/js/model/place-order-mixin': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            shim: {
                cardinaljs: {
                    exports: 'Cardinal'
                },
                cardinaljsSandbox: {
                    exports: 'Cardinal'
                }
            },
            paths: {
                cardinaljsSandbox: 'https://includestest.ccdc02.com/cardinalcruise/v1/songbird',
                cardinaljs: 'https://songbird.cardinalcommerce.com/edge/v1/songbird'
            }
        };


        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    transparent: 'Magento_Payment/js/transparent',
                    'Magento_Payment/transparent': 'Magento_Payment/js/transparent'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    orderReview: 'Magento_Paypal/js/order-review',
                    'Magento_Paypal/order-review': 'Magento_Paypal/js/order-review',
                    paypalCheckout: 'Magento_Paypal/js/paypal-checkout'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            config: {
                mixins: {
                    'Magento_Customer/js/customer-data': {
                        'Magento_Persistent/js/view/customer-data-mixin': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    loadPlayer: 'Magento_ProductVideo/js/load-player',
                    fotoramaVideoEvents: 'Magento_ProductVideo/js/fotorama-add-video-events'
                }
            },
            shim: {
                vimeoAPI: {}
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            config: {
                mixins: {
                    'Magento_Checkout/js/action/place-order': {
                        'Magento_CheckoutAgreements/js/model/place-order-mixin': true
                    },
                    'Magento_Checkout/js/action/set-payment-information': {
                        'Magento_CheckoutAgreements/js/model/set-payment-information-mixin': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        /*eslint strict: ["error", "global"]*/

        'use strict';

        var config = {
            config: {
                mixins: {
                    'Magento_Ui/js/view/messages': {
                        'Magento_ReCaptchaFrontendUi/js/ui-messages-mixin': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

// eslint-disable-next-line no-unused-vars
        var config = {
            config: {
                mixins: {
                    'Magento_Paypal/js/view/payment/method-renderer/payflowpro-method': {
                        'Magento_ReCaptchaPaypal/js/payflowpro-method-mixin': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            shim: {
                'Magento_Tinymce3/tiny_mce/tiny_mce_src': {
                    'exports': 'tinymce'
                }
            },
            map: {
                '*': {
                    'tinymceDeprecated': 'Magento_Tinymce3/tiny_mce/tiny_mce_src'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    editTrigger: 'mage/edit-trigger',
                    addClass: 'Magento_Translation/js/add-class',
                    'Magento_Translation/add-class': 'Magento_Translation/js/add-class'
                }
            },
            deps: [
                'mage/translate-inline'
            ]
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    mageTranslationDictionary: 'Magento_Translation/js/mage-translation-dictionary'
                }
            },
            deps: [
                'mageTranslationDictionary'
            ]
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            config: {
                mixins: {
                    'Magento_Checkout/js/view/payment/list': {
                        'Magento_PaypalCaptcha/js/view/payment/list-mixin': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    'taxToggle': 'Magento_Weee/js/tax-toggle',
                    'Magento_Weee/tax-toggle': 'Magento_Weee/js/tax-toggle'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            map: {
                '*': {
                    wishlist:       'Magento_Wishlist/js/wishlist',
                    addToWishlist:  'Magento_Wishlist/js/add-to-wishlist',
                    wishlistSearch: 'Magento_Wishlist/js/search'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright 2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.
         *
         * Licensed under the Apache License, Version 2.0 (the "License").
         * You may not use this file except in compliance with the License.
         * A copy of the License is located at
         *
         *  http://aws.amazon.com/apache2.0
         *
         * or in the "license" file accompanying this file. This file is distributed
         * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
         * express or implied. See the License for the specific language governing
         * permissions and limitations under the License.
         */

        var config = {
            map: {
                '*': {
                    amazonLogout: 'Amazon_Login/js/amazon-logout',
                    amazonOAuthRedirect: 'Amazon_Login/js/amazon-redirect',
                    amazonCsrf: 'Amazon_Login/js/amazon-csrf'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright 2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.
         *
         * Licensed under the Apache License, Version 2.0 (the "License").
         * You may not use this file except in compliance with the License.
         * A copy of the License is located at
         *
         *  http://aws.amazon.com/apache2.0
         *
         * or in the "license" file accompanying this file. This file is distributed
         * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
         * express or implied. See the License for the specific language governing
         * permissions and limitations under the License.
         */
        var config = {
            map: {
                '*': {
                    amazonCore: 'Amazon_Payment/js/amazon-core',
                    amazonWidgetsLoader: 'Amazon_Payment/js/amazon-widgets-loader',
                    amazonButton: 'Amazon_Payment/js/amazon-button',
                    amazonProductAdd: 'Amazon_Payment/js/amazon-product-add',
                    amazonPaymentConfig: 'Amazon_Payment/js/model/amazonPaymentConfig',
                    sjcl: 'Amazon_Payment/js/lib/sjcl.min'
                }
            },
            config: {
                mixins: {
                    'Amazon_Payment/js/action/place-order': {
                        'Amazon_Payment/js/model/place-order-mixin': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * This file is part of the Klarna KP module
         *
         * (c) Klarna Bank AB (publ)
         *
         * For the full copyright and license information, please view the NOTICE
         * and LICENSE files that were distributed with this source code.
         */
        var config = {
            config: {
                mixins: {
                    'Magento_Checkout/js/action/get-payment-information': {
                        'Klarna_Kp/js/action/override': true
                    }
                }
            },
            map: {
                '*': {
                    klarnapi: 'https://x.klarnacdn.net/kp/lib/v1/api.js'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * This file is part of the Klarna Onsitemessaging module
         *
         * (c) Klarna Bank AB (publ)
         *
         * For the full copyright and license information, please view the NOTICE
         * and LICENSE files that were distributed with this source code.
         */
        var config = {
            config: {
                mixins: {
                    "Magento_Catalog/js/price-box": {
                        "Klarna_Onsitemessaging/js/pricebox-widget-mixin": true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Mageplaza
         *
         * NOTICE OF LICENSE
         *
         * This source file is subject to the mageplaza.com license that is
         * available through the world-wide-web at this URL:
         * https://www.mageplaza.com/LICENSE.txt
         *
         * DISCLAIMER
         *
         * Do not edit or add to this file if you wish to upgrade this extension to newer
         * version in the future.
         *
         * @category    Mageplaza
         * @package     Mageplaza_Core
         * @copyright   Copyright (c) Mageplaza (https://www.mageplaza.com/)
         * @license     https://www.mageplaza.com/LICENSE.txt
         */

        var config = {
            paths: {
                'mageplaza/core/jquery/popup': 'Mageplaza_Core/js/jquery.magnific-popup.min',
                'mageplaza/core/owl.carousel': 'Mageplaza_Core/js/owl.carousel.min',
                'mageplaza/core/bootstrap': 'Mageplaza_Core/js/bootstrap.min',
                mpIonRangeSlider: 'Mageplaza_Core/js/ion.rangeSlider.min',
                touchPunch: 'Mageplaza_Core/js/jquery.ui.touch-punch.min',
                mpDevbridgeAutocomplete: 'Mageplaza_Core/js/jquery.autocomplete.min'
            },
            shim: {
                "mageplaza/core/jquery/popup": ["jquery"],
                "mageplaza/core/owl.carousel": ["jquery"],
                "mageplaza/core/bootstrap": ["jquery"],
                mpIonRangeSlider: ["jquery"],
                mpDevbridgeAutocomplete: ["jquery"],
                touchPunch: ['jquery', 'jquery/ui']
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Mageplaza
         *
         * NOTICE OF LICENSE
         *
         * This source file is subject to the Mageplaza.com license that is
         * available through the world-wide-web at this URL:
         * https://www.mageplaza.com/LICENSE.txt
         *
         * DISCLAIMER
         *
         * Do not edit or add to this file if you wish to upgrade this extension to newer
         * version in the future.
         *
         * @category    Mageplaza
         * @package     Mageplaza_AjaxLayer
         * @copyright   Copyright (c) Mageplaza (http://www.mageplaza.com/)
         * @license     https://www.mageplaza.com/LICENSE.txt
         */

        var config = {
            paths: {
                mpAjax: 'Mageplaza_AjaxLayer/js/view/layer'
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Mageplaza
         *
         * NOTICE OF LICENSE
         *
         * This source file is subject to the Mageplaza.com license sliderConfig is
         * available through the world-wide-web at this URL:
         * https://www.mageplaza.com/LICENSE.txt
         *
         * DISCLAIMER
         *
         * Do not edit or add to this file if you wish to upgrade this extension to newer
         * version in the future.
         *
         * @category    Mageplaza
         * @package     Mageplaza_LayeredNavigation
         * @copyright   Copyright (c) Mageplaza (https://www.mageplaza.com/)
         * @license     https://www.mageplaza.com/LICENSE.txt
         */

        var config = {
            paths: {
                mpLayer: 'Mageplaza_LayeredNavigation/js/view/layer'
            },
            shim: {
                mpLayer: ['touchPunch']
            }
        };

        require.config(config);
    })();
    (function() {
        var config = {
            'map'   : {
                '*' : {
                    /**
                     * todo: remove this when update recaptcha module
                     */
                    'MSP_ReCaptcha/js/reCaptcha' : 'Printq_Core/js/reCaptcha'
                }
            },
            'paths': {
                'printq_fancybox': 'fancybox/jquery.fancybox.pack',
                /*'printq_select2': 'printq/select/select2.min',
                'printq_draggableBackground': 'printq/oie/jquery-draggable-background/draggable_background',
                'printq_jquery_pep': 'printq/oie/jquery.pep/jquery.pep',
                'printq_jcarousel': 'jcarousel/jquery.jcarousel.min',
                'printq_ui_rotatable': 'printq/oie/jquery-ui/ui-rotatable/jquery.ui.rotatable',
                'printq_jquery_cropper': 'printq/oie/jquery-cropper/jquery-cropper.min',
                'cropperjs': 'printq/oie/cropperjs/cropper.min',
                'printq_rotatable_patch_oie': 'printq/oie/jquery/resizable-rotatable.patch',
                'printq_patch_draggable': 'printq/oie/jquery/patch_draggable',
                'printq_justifiedGallery': 'printq/oie/justifiedGallery/jquery.justifiedGallery.min',
                'printq_ramda': 'printq/oie/ramda/ramda',
                'jquery_ui': 'printq/oie/jquery-ui/jquery-ui-1.11.4',
                'Magento_Ui/js/form/element/file-uploader': 'Printq_Core/js/form/element/file-uploader'*/
            },
            'shim': {
                'printq_fancybox': {
                    exports: 'printq_fancybox',
                    'deps': ['jquery','jquery/ui']
                },
                /* 'printq_select2': {
                     exports: 'printq_select2',
                     'deps': ['jquery', 'jquery/ui']
                 },
                 'printq_draggableBackground': {
                     exports: 'printq_draggableBackground',
                     'deps': ['jquery','jquery/ui']
                 },
                 'printq_jquery_pep': {
                     exports: 'printq_jquery_pep',
                     'deps': ['jquery', 'jquery/ui']
                 },
                 'printq_jcarousel': {
                     exports: 'printq_jcarousel',
                     'deps': ['jquery', 'jquery/ui']
                 },
                 'printq_ui_rotatable': {
                     exports: 'printq_ui_rotatable',
                     'deps': ['jquery', 'jquery/ui']
                 },
                 'printq_jquery_cropper': {
                     exports: 'printq_jquery_cropper',
                     'deps': ['jquery', 'jquery/ui']
                 },
                 'cropperjs': {
                     exports: 'cropperjs',
                     'deps': ['jquery', 'jquery/ui']
                 },
                 'printq_rotatable_patch_oie': {
                     'deps': ['jquery', 'jquery/ui']
                 },
                 'printq_patch_draggable': {
                     exports: 'printq_patch_draggable',
                     'deps': ['jquery', 'jquery/ui']
                 },
                 'printq_ramda': {
                     exports: 'printq_ramda',
                     'deps': ['jquery', 'jquery/ui']
                 },
                 'jquery_ui': {
                     exports: 'printq_ramda',
                     'deps': ['jquery', 'jquery/ui']
                 }*/
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * @copyright  Vertex. All rights reserved.  https://www.vertexinc.com/
         * @author     Mediotype                     https://www.mediotype.com/
         */

        var config = {
            map: {
                '*': {
                    'set-checkout-messages': 'Vertex_Tax/js/model/set-checkout-messages'
                }
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * @copyright  Vertex. All rights reserved.  https://www.vertexinc.com/
         * @author     Mediotype                     https://www.mediotype.com/
         */

        var config = {
            config: {
                mixins: {
                    'Magento_Checkout/js/view/billing-address': {
                        'Vertex_AddressValidation/js/billing-validation-mixin': true
                    },
                    'Magento_Checkout/js/view/shipping': {
                        'Vertex_AddressValidation/js/shipping-validation-mixin': true
                    },
                    'Magento_Checkout/js/checkout-data': {
                        'Vertex_AddressValidation/js/shipping-invalidate-mixin': true
                    },
                    'Magento_Customer/js/addressValidation': {
                        'Vertex_AddressValidation/js/customer-validation-mixin': true
                    }
                }
            }
        };

        require.config(config);
    })();
    (function() {
        var config = {
            config: {
                mixins: {
                    'catalogAddToCart': {
                        'Printq_WebProduct/js/catalog-add-to-cart-mixin': true
                    },
                    'Magento_Catalog/js/catalog-add-to-cart': {
                        'Printq_WebProduct/js/catalog-add-to-cart-mixin': true
                    }
                },
            },
            map: {
                '*': {
                    'Magento_Checkout/template/minicart/item/default.html' : 'Printq_WebProduct/template/minicart/item/default.html',
                }
            },
            paths: {
                'printqMatrixApp': 'Printq_WebProduct/js/printq_app'
            },
            shim: {
                'printqMatrixApp': {
                    'deps': ['Printq_WebProduct/js/printq_app_chunk-vendors']
                },
            }
        };

        require.config(config);
    })();
    (function() {
        /**
         * Copyright © Magento, Inc. All rights reserved.
         * See COPYING.txt for license details.
         */

        var config = {
            deps: [
                'Magento_Theme/js/theme'
            ]
        };

        require.config(config);
    })();
    (function() {
        var config = {
            paths: {
                'imagesloaded': 'Smartwave_Porto/js/imagesloaded',
                'packery': 'Smartwave_Porto/js/packery.pkgd',
                'instafeed': 'Smartwave_Porto/js/instafeed.min'
            },
            shim: {
                'imagesloaded': {
                    deps: ['jquery']
                },
                'packery': {
                    deps: ['jquery']
                },
                'instafeed': {
                    deps: ['jquery']
                }
            }
        };

        require.config(config);
    })();



})(require);
