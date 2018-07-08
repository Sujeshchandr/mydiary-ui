var DRTestDataTable = (function () {

    var $ExpensesTable = $('#tblExpenses');
    var expensesDataTable = null;

    $(function () {

        $(document).on("click", '.applyAllFilters', function () {
            expensesDataTable.ajax.reload(function (json) {
                console.log("all filters applied")
            });
        });

        moment.updateLocale('en', {
            calendar: {
                lastDay: '[Yesterday]',
                sameDay: '[Today]',
                nextDay: '[Tomorrow]',
                thisMonth: '[This Month]',
                nextMonth: '[Next Month]',
                lastWeek: 'LLL',
                nextWeek: 'LLL',
                sameElse: 'LLL'
            },
            longDateFormat: {
                LT: "h:mm A",
                LTS: "h:mm:ss A",
                L: "MM/DD/YYYY",
                l: "M/D/YYYY",
                LL: "MMMM Do YYYY",
                ll: "MMM D YYYY",
                LLL: "MMMM Do YYYY LT",
                lll: "MMM D YYYY LT",
                LLLL: "dddd, MMMM Do YYYY LT",
                llll: "ddd, MMM D YYYY LT"
            },
            relativeTime: {
                future: "in %s",
                past: "%s ago",
                s: "seconds",
                m: "a minute",
                mm: "%d minutes",
                h: "an hour",
                hh: "%d hours",
                d: "a day",
                dd: "%d days",
                M: "a month",
                MM: "%d months",
                y: "a year",
                yy: "%d years"
            },
            calendarFormat: function (myMoment, now) {
                var diff = myMoment.diff(now, 'days', true);
                var nextMonth = now.clone().add(1, 'month');

                var retVal = diff < -6 ? 'sameElse' :
                    diff < -1 ? 'lastWeek' :
                    diff < 0 ? 'lastDays' :
                    diff < 1 ? 'sameDay' :
                    diff < 2 ? 'nextDay' :
                    diff < 7 ? 'nextWeek' :

                    // introduce two custom labels thisMonth and nextMonth
                    (myMoment.month() === now.month() && myMoment.year() === now.year()) ? 'thisMonth' :
                    (nextMonth.month() === myMoment.month() && nextMonth.year() === myMoment.year()) ? 'nextMonth' : 'sameElse';
                return retVal;
            }
        });

        expensesDataTable = $ExpensesTable.
                            DataTable(
                             {
                                 processing: true, // whether processing..... text needs to show or not
                                 serverSide: true,
                                 filter: false,
                                 paging: true,
                                 cache: false,
                                 pagingType: "full_numbers",
                                 lengthMenu: [[-1, 10, 25, 50], ["All", 10, 25, 50]],
                                 pageLength: 10,
                                 ajax: {
                                     type: 'POST',
                                     url: "getDataTableJson",
                                     dataSrc: "employees",
                                     complete: function (response) {
                                     },
                                     data: function (d) {

                                         //ToDo --> add custom filter values as new properties
                                         d.statusIds = [1, 2, 3];
                                         d.journalId = 600;
                                         d.repositoryId = 1;
                                         d.publishedstartDate = '';
                                         d.publishedendDate = '';
                                         d.depositingstartDate = '';
                                         d.depositingendDate = '';
                                         d.doiSearchText = '';

                                         return d;
                                     },
                                     dataFilter: function (d) {

                                         //var json = jQuery.parseJSON(data);
                                         ////json.recordsTotal = json.employees.length;
                                         //json.recordsFiltered = json.employees.length;
                                         //json.data = json.employees;

                                         //return JSON.stringify(json);
                                         return d;
                                     }
                                 },
                                 language: {
                                     decimal: "",
                                     emptyTable: "No data available in table",
                                     info: "Showing _START_ to _END_ of _TOTAL_ entries",
                                     infoEmpty: "",//default : "Showing 0 to 0 of 0 entries"
                                     infoFiltered: "(filtered from _MAX_ total entries)",
                                     infoPostFix: "",
                                     thousands: ",",
                                     lengthMenu: "Showing _MENU_ entries",//default :"Show _MENU_ entries"
                                     loadingRecords: "Loading...",
                                     processing: "Processing...",
                                     search: "Search:",
                                     zeroRecords: "No matching records found",
                                     paginate: {
                                         first: "First",
                                         last: "Last",
                                         next: "Next",
                                         previous: "Previous"
                                     },
                                     aria: {
                                         sortAscending: ": activate to sort column ascending",
                                         sortDescending: ": activate to sort column descending"
                                     }
                                 },
                                 deferRender: true,
                                 columnDefs: [

                                     { title: "Name", targets: 0 },
                                     { title: "Position", targets: 1 },
                                     { title: "Office", targets: 2 },
                                     { title: "Extn.", targets: 3 },
                                     { title: "Start date", targets: 4 },
                                     { title: "Salary", targets: 5 }
                                 ],
                                 columns: [
                                     { name: "Name", data: "name" },
                                     { name: "Position", data: "position" },
                                     { name: "Office", data: "office" },
                                     { name: "Extn", data: "extn" },
                                     {
                                         name: "StartDate",
                                         data: "start_date",
                                         render: function (data) {
                                             return moment(data).calendar();
                                         }
                                     },
                                     { name: "Salary", data: "salary" }
                                 ],
                             });
    });

    return {

    };
})();