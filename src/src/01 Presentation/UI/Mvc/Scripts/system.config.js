/**
 * System configuration for Angular samples
 * Adjust as necessary for your application needs.
 */
(function (global) {
    System.config({

        //use typescript for compilation
        transpiler: 'typescript',
        //typescript compiler options
        typescriptOptions: {
            emitDecoratorMetadata: true
        },
        paths: {
            // paths serve as alias
            'npm:': '/node_modules/', //  local packagemanager url 
            //'npm:': 'https://unpkg.com/' // nuget packagemanager url
            'service:': '/Scripts/Diary/Home/Angular2/Services/'

        },
        // map tells the System loader where to look for things
        map: {

            'homePage': '/Scripts/Diary/Home/Angular2',  // our app is within the this folder

             //// transpiler module
            'typescript': 'npm:typescript@2.0.2/lib/typescript.js',

             //// angular modules
            '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
            '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
            '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
            '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
            '@angular/http': 'npm:@angular/http/bundles/http.umd.js',
            '@angular/router': 'npm:@angular/router/bundles/router.umd.js',
            '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',

             //// other modules
            'rxjs': 'npm:rxjs',
            'ng2-charts': 'npm:ng2-charts', //https://plnkr.co/edit/edlnC7yLjpSmLF1RFjVB?p=preview,
             //'angular-in-memory-web-api': 'npm:angular-in-memory-web-api/bundles/in-memory-web-api.umd.js',
            //'@homeService': './Services/diary.homeService.js'

        },
        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            'homePage': {
                main: 'diary.main',
                defaultExtension: 'js'
            },
            'rxjs': {
                defaultExtension: 'js'
            },
            'ng2-charts': {
                main: "ng2-charts.js",
                defaultExtension: 'js'
            }
            //,
            //'homeService': {
            //    main: "diary.homeService",
            //    defaultExtension: 'js'
            //}
        }
    });
})(this);
