System.register(["@angular/platform-browser-dynamic", "./diary.homeModule"], function (exports_1, context_1) {
    "use strict";
    var platform_browser_dynamic_1, diary_homeModule_1;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (platform_browser_dynamic_1_1) {
                platform_browser_dynamic_1 = platform_browser_dynamic_1_1;
            },
            function (diary_homeModule_1_1) {
                diary_homeModule_1 = diary_homeModule_1_1;
            }
        ],
        execute: function () {
            platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(diary_homeModule_1.AppModule);
        }
    };
});
//# sourceMappingURL=diary.main.js.map