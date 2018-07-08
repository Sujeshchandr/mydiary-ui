using System.Web;
using System.Web.Optimization;

namespace MyDiary.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region JAVA SCRIPT 

            #region JQUERY

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include( "~/Scripts/JQuery/jquery-{version}.js")
                  .Include("~/Scripts/JQuery/jquery.unobstrusive-ajax.js")
                .Include("~/Scripts/JQuery/jquery.batch.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/JQuery/JQueryUI/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/JQuery/JQueryValidate/jquery.unobtrusive*",
                        "~/Scripts/JQuery/JQueryValidate/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/JqueryFileUploader")
                 .Include("~/Scripts/JQuery/JQueryFileUploader/jquery.fileupload.js")
                 );

            bundles.Add(new ScriptBundle("~/bundles/JqueryUIWidget")
                .Include("~/Scripts/JQuery/JQueryWidget/jquery.ui.widget.js")
                );
            bundles.Add(new ScriptBundle("~/bundles/JQueryImageResize")
                .Include("~/Scripts/JQuery/JQueryImageResize/jquery.ae.image.resize.js")
                );

            #endregion

            #region ANGULAR JS

            bundles.Add(new ScriptBundle("~/bundles/angularjs")
                        .Include("~/Scripts/AngularJS/angular-{version}.js")
                        );
            bundles.Add(new ScriptBundle("~/bundles/angular.ui")
                       //.Include("~/Scripts/AngularJS/UI/Bootstrap/ui-bootstrap-{version}.js") //doesn't containt the templates.
                       .Include("~/Scripts/AngularJS/UI/Bootstrap/ui-bootstrap-tpls-{version}.js") //Contains the templates also.
                       .Include("~/Scripts/AngularJS/UI/Select/ui.select.js")
                       .Include("~/Scripts/AngularJS/UI/Sanitize/angular-sanitize.js")
                       .Include("~/Scripts/AngularJS/UI/Ng-Grid/ng-grid.js")
                       .Include("~/Scripts/AngularJS/UI/Ng-Grid/ng-grid-csv-export.js")
                       .Include("~/Scripts/AngularJS/UI/Bootstrap/Switch/angular-bootstrap-switch.js")
                       .Include("~/Scripts/AngularJS/UI/Chart/angular-chart.js")
                       // .Include("~/Scripts/AngularJS/UI/Bootstrap/DatePicker/datepicker.js")
                       );

            #endregion

            #region BOOTSTRAP JS

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                         .Include("~/Scripts/Bootstrap/bootstrap.js")) ;
            bundles.Add(new ScriptBundle("~/bundles/bootstrap.ui")
                  .Include("~/Scripts/Bootstrap/UI/Select/bootstrap-select.js" )
                  .Include("~/Scripts/Bootstrap/UI/Switch/bootstrap-switch.js") 
                  );

            #endregion

            #region SELECT2 JS
            bundles.Add(new ScriptBundle("~/bundles/select2")
                         .Include("~/Scripts/Select2/select2.js"));
            #endregion

            #region DIARY JS

            bundles.Add(new ScriptBundle("~/bundles/Diary-IBar")
                   //.Include("~/Scripts/Diary/Common/diary.error.js")
                   //.Include("~/Scripts/Diary/Common/diary.log.js")
                   //.Include("~/Scripts/Diary/Common/diary.alert.js")
                   //.Include("~/Scripts/Diary/Common/diary.popup.js")
                   .Include("~/Scripts/Diary/Common/diary.ui.js")
                   .Include("~/Scripts/Diary/Common/diary.common.js")
                   .Include("~/Scripts/Diary/Common/diary.validate.js")
                   .Include("~/Scripts/Diary/Common/diary.layout.js")
                   .Include("~/Scripts/Diary/Common/diary.enums.js")
                   .Include("~/Scripts/Diary/Common/diary.ie.js")
                   //.Include("~/Scripts/Diary/ActivityArea/diary.activityarea.js")
                   //.Include("~/Scripts/Diary/ToolBox/diary.toolbox.js")
                   .Include("~/Scripts/Diary/User/diary.user.js"));

            bundles.Add(new ScriptBundle("~/bundles/Diary")
                       .Include("~/Scripts/Diary/Common/diary.error.js")
                       .Include("~/Scripts/Diary/Common/diary.log.js")
                       .Include("~/Scripts/Diary/Common/diary.alert.js")
                       .Include("~/Scripts/Diary/Common/diary.popup.js")
                       .Include("~/Scripts/Diary/ActivityArea/diary.activityarea.js")
                       .Include("~/Scripts/Diary/ToolBox/diary.toolbox.js")
                       .Include("~/Scripts/Diary/User/diary.image.js")
                       .Include("~/Scripts/Diary/Extensions/dateTime.extension.js"));

            bundles.Add(new ScriptBundle("~/bundles/Diary/angularjs")
             .Include("~/Scripts/Diary/Angular/diary.AngularModule.js")
             .Include("~/Scripts/Diary/Angular/diary.AngularTemplates.js")
             .Include("~/Scripts/Diary/Angular/Ui/diary.uiModule.js")
             .Include("~/Scripts/Diary/Angular/Ui/diary.uiController.js")
             .Include("~/Scripts/Diary/Angular/Common/diary.commonModule.js")
             .Include("~/Scripts/Diary/Angular/Batch/angular-http-batch.js")//TODO--> NOT TESTED COMPLETEDLY
           );

            bundles.Add(new ScriptBundle("~/bundles/Diary.Home")
                   .Include("~/Scripts/Diary/Home/diary.homeModule.js")
                   .Include("~/Scripts/Diary/Home/diary.homeController.js")
                    .Include("~/Scripts/Diary/Home/diary.homeKnockOut.js")
                   );

            bundles.Add(new ScriptBundle("~/bundles/Diary.Expenses")
                    .Include("~/Scripts/Diary/Expenses/diary.expenseModule.js")
                    .Include("~/Scripts/Diary/Expenses/diary.expenseController.js")
                    );
            bundles.Add(new ScriptBundle("~/bundles/Diary.Incomes")
                   .Include("~/Scripts/Diary/Incomes/diary.incomeModule.js")   
                   .Include("~/Scripts/Diary/Incomes/diary.incomeController.js")                  
                   );
            bundles.Add(new ScriptBundle("~/bundles/Diary.User")
                    //.Include("~/Scripts/Diary/User/diary.user.js")
                    .Include("~/Scripts/Diary/Common/diary.ui.js")
                    . Include("~/Scripts/JQuery/JQueryWidget/jquery.ui.widget.js")
                    .Include("~/Scripts/FaceBookAPI/facebookAPI.js")
                    );

            #endregion

            #region CHART JS
             bundles.Add(new ScriptBundle("~/bundles/chart")
                       .Include("~/Scripts/ChartJs/chart.js"));
            #endregion

            #region KNOCKOUT JS
             bundles.Add(new ScriptBundle("~/bundles/knockout")
                    .Include("~/Scripts/KnockOut/knockout-{version}.js"));
             #endregion

           #endregion 

            #region CASCADE STYLE SHEET

             #region JQUERY CSS
             bundles.Add(new StyleBundle("~/Content/themes/base/css")
                    .Include("~/Content/themes/base/jquery.ui.core.css",
                                "~/Content/themes/base/jquery.ui.resizable.css",
                                "~/Content/themes/base/jquery.ui.selectable.css",
                                "~/Content/themes/base/jquery.ui.accordion.css",
                                "~/Content/themes/base/jquery.ui.autocomplete.css",
                                "~/Content/themes/base/jquery.ui.button.css",
                                "~/Content/themes/base/jquery.ui.dialog.css",
                                "~/Content/themes/base/jquery.ui.slider.css",
                                "~/Content/themes/base/jquery.ui.tabs.css",
                                "~/Content/themes/base/jquery.ui.datepicker.css",
                                "~/Content/themes/base/jquery.ui.progressbar.css",
                                "~/Content/themes/base/jquery.ui.theme.css",
                                "~/Content/themes/base/jquery.fileupload.css"));
            #endregion                       
          
            #region ANGULAR CSS
            bundles.Add(new StyleBundle("~/Content/nggrid")
                           .Include("~/Content/AngularJS/Ng-Grid/ng-grid.css")
                           .Include("~/Content/AngularJS/UI/Modal/ui.modal.css")
                           .Include("~/Content/AngularJS/UI/Select/ui.select.css")
                           .Include("~/Content/AngularJS/Chart/angular-chart.css")
                           );
            #endregion

            #region BOOTSTRAP CSS

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/Bootstrap/bootstrap.css")
                                                              .Include("~/Content/Bootstrap/bootstrap-theme.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap.select").Include("~/Content/Bootstrap/Select/bootstrap-select.css")
                                                              );
            bundles.Add(new StyleBundle("~/Content/bootstrap.select2").Include("~/Content/Bootstrap/Select2/select2-bootstrap.css")
                                                              );
            bundles.Add(new StyleBundle("~/Content/bootstrap.switch").Include("~/Content/Bootstrap/Switch/bootstrap3/bootstrap-switch.css"));

            #endregion           

            #region SELECT2
            bundles.Add(new StyleBundle("~/Content/select2")
                                        .Include("~/Content/Select2/select2.css") );
            #endregion

            #region DIARY CSS

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Diary")
                .Include("~/Content/Diary/Common/DRNotify.Messages.css")
                .Include("~/Content/Diary/Common/DRPopup.css")
                );
            bundles.Add(new StyleBundle("~/Content/Diary.User")
                        .Include("~/Content/Diary/diary.user.css")
                        );

            bundles.Add(new StyleBundle("~/Content/Diary.Home")
                        .Include("~/Content/Diary/diary.home.css")
                        );

            bundles.Add(new StyleBundle("~/Content/Diary.Income")
                        .Include("~/Content/Diary/diary.incomes.css")
                        );
            bundles.Add(new StyleBundle("~/Content/Diary.Expense")
                       .Include("~/Content/Diary/diary.expenses.css")
                       );
            #endregion           

            #endregion

        }
    }
}