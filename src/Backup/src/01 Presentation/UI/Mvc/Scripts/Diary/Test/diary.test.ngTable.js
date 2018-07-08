
var DRAngularNgModule = angular.module("myApp", ["ngTable"]);


DRAngularNgModule.controller('testNgTableController', testNgTableController);

testNgTableController.$inject = ['$scope', 'NgTableParams', '$http', '$filter'];

function testNgTableController($scope, NgTableParams, $http, $filter) {

    var self = $scope;

    self.ShowFilter = false;

    self.tableColumnDefinitions = [
         { show: true, field: "extn", title: "Extn:", titleAlt: 'Extn:', filter: { extn: "text" }, sortable: "extn", headerTitle: 'Extension', class: "blue-column" },
         { show: true, field: "name", title: "Name", titleAlt: 'Extn:', filter: { name: "text" }, sortable: "name", headerTitle: 'Name', class: "blue-column" },
         { show: true, field: "office", title: "Office", titleAlt: 'Extn:', filter: { office: "text" }, sortable: "office", headerTitle: 'Office', class: "blue-column" },
         { show: true, field: "position", title: "Position", titleAlt: 'Extn:', filter: { position: "text" }, sortable: "position", headerTitle: 'Position', class: "blue-column" },
         { show: true, field: "salary", title: "Salary", titleAlt: 'Extn:', filter: { salary: "text" }, sortable: "salary", headerTitle: 'Salary', class: "blue-column" },
         //{ field: "extn", title: "Extn:", filter: { extn: "text" }, show: true },
    ];

    self.tableParams = getNgTableConfigurationOptions();

    ///http://reactivex.io/learnrx/
    self.process = function () {
        console.log(exercise11());
    };

    Array.prototype.concatAll = function () {
        var results = [];
        this.forEach(function (subArray) {
            results.push.apply(results, subArray);
        });

        return results;
    };

    function exercise1() {
        var names = ["Ben", "Jafar", "Matt", "Priya", "Brian"],
		counter;

        for (counter = 0; counter < names.length; counter++) {
            console.log(names[counter]);
        }
    };   // Print all the names in an array

    function exercise2() {
        var names = ["Ben", "Jafar", "Matt", "Priya", "Brian"];

        names.forEach(function (name) {
            console.log(name);
        });
    };   // Use forEach to print all the names in an array

    function exercise3() {
        var newReleases = [
			{
			    "id": 70111470,
			    "title": "Die Hard",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/DieHard.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": [4.0],
			    "bookmark": []
			},
			{
			    "id": 654356453,
			    "title": "Bad Boys",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/BadBoys.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": [5.0],
			    "bookmark": [{ id: 432534, time: 65876586 }]
			},
			{
			    "id": 65432445,
			    "title": "The Chamber",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/TheChamber.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": [4.0],
			    "bookmark": []
			},
			{
			    "id": 675465,
			    "title": "Fracture",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/Fracture.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": [5.0],
			    "bookmark": [{ id: 432534, time: 65876586 }]
			}
        ];

        var videoAndTitlePairs = [];

        newReleases.forEach(function (video) {
            videoAndTitlePairs.push({ id: video.id, title: video.title });
        });

        return videoAndTitlePairs;
    };  // Project an array of videos into an array of {id,title} pairs using forEach()

    function exercise4() {
        //Array.prototype.map = function (projectionFunction) {
        //    var results = [];
        //    this.forEach(function (itemInArray) {
        //        results.push(projectionFunction(itemInArray));

        //    });

        //    return results;
        //};


    };  // Implement map()

    function exercise5() {

        var newReleases = [
			{
			    "id": 70111470,
			    "title": "Die Hard",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/DieHard.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": [4.0],
			    "bookmark": []
			},
			{
			    "id": 654356453,
			    "title": "Bad Boys",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/BadBoys.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": [5.0],
			    "bookmark": [{ id: 432534, time: 65876586 }]
			},
			{
			    "id": 65432445,
			    "title": "The Chamber",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/TheChamber.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": [4.0],
			    "bookmark": []
			},
			{
			    "id": 675465,
			    "title": "Fracture",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/Fracture.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": [5.0],
			    "bookmark": [{ id: 432534, time: 65876586 }]
			}
        ];

        return newReleases.map(function (video) {

            return { id: video.id, title: video.title };
        });

    };  // Use map() to project an array of videos into an array of {id,title} pairs

    function exercise6() {

        var newReleases = [
			{
			    "id": 70111470,
			    "title": "Die Hard",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/DieHard.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": 4.0,
			    "bookmark": []
			},
			{
			    "id": 654356453,
			    "title": "Bad Boys",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/BadBoys.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": 5.0,
			    "bookmark": [{ id: 432534, time: 65876586 }]
			},
			{
			    "id": 65432445,
			    "title": "The Chamber",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/TheChamber.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": 4.0,
			    "bookmark": []
			},
			{
			    "id": 675465,
			    "title": "Fracture",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/Fracture.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": 5.0,
			    "bookmark": [{ id: 432534, time: 65876586 }]
			}
        ];

        var videos = [];

        newReleases.forEach(function (video) {
            if (video.rating == '5.0') {
                videos.push(video);
            };
        });

        return videos;
    };  // Use forEach() to collect only those videos with a rating of 5.0

    function exercise7() {
        Array.prototype.filter = function (predicateFunction) {
            var results = [];
            this.forEach(function (itemInArray) {
                if (predicateFunction(itemInArray)) {
                    results.push(itemInArray);
                };
            });

            return results;
        };
    };  // Implement filter()

    function exercise8() {

        var newReleases = [
			{
			    "id": 70111470,
			    "title": "Die Hard",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/DieHard.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": 4.0,
			    "bookmark": []
			},
			{
			    "id": 654356453,
			    "title": "Bad Boys",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/BadBoys.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": 5.0,
			    "bookmark": [{ id: 432534, time: 65876586 }]
			},
			{
			    "id": 65432445,
			    "title": "The Chamber",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/TheChamber.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": 4.0,
			    "bookmark": []
			},
			{
			    "id": 675465,
			    "title": "Fracture",
			    "boxart": "http://cdn-0.nflximg.com/images/2891/Fracture.jpg",
			    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
			    "rating": 5.0,
			    "bookmark": [{ id: 432534, time: 65876586 }]
			}
        ];

        return newReleases
              .filter(function (itemInArray) {
                  return (itemInArray.rating == '5.0')
              })
              .map(function (filteredItem) {
                  return filteredItem.id;
              });

    }; // Chain filter and map to collect the ids of videos that have a rating of 5.0

    function exercise9() {

        var movieLists = [
			{
			    name: "New Releases",
			    videos: [
					{
					    "id": 70111470,
					    "title": "Die Hard",
					    "boxart": "http://cdn-0.nflximg.com/images/2891/DieHard.jpg",
					    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
					    "rating": 4.0,
					    "bookmark": []
					},
					{
					    "id": 654356453,
					    "title": "Bad Boys",
					    "boxart": "http://cdn-0.nflximg.com/images/2891/BadBoys.jpg",
					    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
					    "rating": 5.0,
					    "bookmark": [{ id: 432534, time: 65876586 }]
					}
			    ]
			},
			{
			    name: "Dramas",
			    videos: [
					{
					    "id": 65432445,
					    "title": "The Chamber",
					    "boxart": "http://cdn-0.nflximg.com/images/2891/TheChamber.jpg",
					    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
					    "rating": 4.0,
					    "bookmark": []
					},
					{
					    "id": 675465,
					    "title": "Fracture",
					    "boxart": "http://cdn-0.nflximg.com/images/2891/Fracture.jpg",
					    "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
					    "rating": 5.0,
					    "bookmark": [{ id: 432534, time: 65876586 }]
					}
			    ]
			}
        ];

        var results = [];

        movieLists
            .map(function (movieList) {
                movieList.videos
                 .map(function (filteredVideo) {
                     results.push(filteredVideo.id);
                 });
            });

        return results;

    }; // Flatten the movieLists array into an array of video ids

    function exercise10() {

        var results = [];
        Array.prototype.concatAll = function (arrayList) {

            this.forEach(function (subArray) {
                results.push.apply(results, subArray);
            });
        };

    }; // Implement concatAll()

    function exercise11() {

        var movieLists = [
                {
                    name: "New Releases",
                    videos: [
                        {
                            "id": 70111470,
                            "title": "Die Hard",
                            "boxart": "http://cdn-0.nflximg.com/images/2891/DieHard.jpg",
                            "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
                            "rating": 4.0,
                            "bookmark": []
                        },
                        {
                            "id": 654356453,
                            "title": "Bad Boys",
                            "boxart": "http://cdn-0.nflximg.com/images/2891/BadBoys.jpg",
                            "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
                            "rating": 5.0,
                            "bookmark": [{ id: 432534, time: 65876586 }]
                        }
                    ]
                },
                {
                    name: "Dramas",
                    videos: [
                        {
                            "id": 65432445,
                            "title": "The Chamber",
                            "boxart": "http://cdn-0.nflximg.com/images/2891/TheChamber.jpg",
                            "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
                            "rating": 4.0,
                            "bookmark": []
                        },
                        {
                            "id": 675465,
                            "title": "Fracture",
                            "boxart": "http://cdn-0.nflximg.com/images/2891/Fracture.jpg",
                            "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
                            "rating": 5.0,
                            "bookmark": [{ id: 432534, time: 65876586 }]
                        }
                    ]
                }
        ];

        //console.log(movieLists);

        return movieLists
            .map(function (movieList) {
                return movieList.videos
                    .map(function (video) {

                return video.id;
            });
        }).concatAll();

        var result = movieLists.
                      map(function (movieList) {
                          return movieList.videos.map(function (video) {
                              return video.id;
                          });
                      }).concatAll();

        //concatAll definitions in two ways

        // first way
        result.forEach(function (arrayItemInMovieList) {
            arrayItemInMovieList.forEach(function (itemInList) {
                result1.push(itemInList);
            });
        });

        // second way
        result.forEach(function (arrayItemInMovieList) {
            result1.push.apply(result1, arrayItemInMovieList);
        });

        console.log(result);
    }; // Use map() and concatAll() to project and flatten the movieLists into an array of video ids

    function exercise12() {

        var movieLists = [
			{
			    name: "Instant Queue",
			    videos: [
					{
					    "id": 70111470,
					    "title": "Die Hard",
					    "boxarts": [
							{ width: 150, height: 200, url: "http://cdn-0.nflximg.com/images/2891/DieHard150.jpg" },
							{ width: 200, height: 200, url: "http://cdn-0.nflximg.com/images/2891/DieHard200.jpg" }
					    ],
					    "url": "http://api.netflix.com/catalog/titles/movies/70111470",
					    "rating": 4.0,
					    "bookmark": []
					},
					{
					    "id": 654356453,
					    "title": "Bad Boys",
					    "boxarts": [
							{ width: 200, height: 200, url: "http://cdn-0.nflximg.com/images/2891/BadBoys200.jpg" },
							{ width: 150, height: 200, url: "http://cdn-0.nflximg.com/images/2891/BadBoys150.jpg" }

					    ],
					    "url": "http://api.netflix.com/catalog/titles/movies/70111470",
					    "rating": 5.0,
					    "bookmark": [{ id: 432534, time: 65876586 }]
					}
			    ]
			},
			{
			    name: "New Releases",
			    videos: [
					{
					    "id": 65432445,
					    "title": "The Chamber",
					    "boxarts": [
							{ width: 150, height: 200, url: "http://cdn-0.nflximg.com/images/2891/TheChamber150.jpg" },
							{ width: 200, height: 200, url: "http://cdn-0.nflximg.com/images/2891/TheChamber200.jpg" }
					    ],
					    "url": "http://api.netflix.com/catalog/titles/movies/70111470",
					    "rating": 4.0,
					    "bookmark": []
					},
					{
					    "id": 675465,
					    "title": "Fracture",
					    "boxarts": [
							{ width: 200, height: 200, url: "http://cdn-0.nflximg.com/images/2891/Fracture200.jpg" },
							{ width: 150, height: 200, url: "http://cdn-0.nflximg.com/images/2891/Fracture150.jpg" },
							{ width: 300, height: 200, url: "http://cdn-0.nflximg.com/images/2891/Fracture300.jpg" }
					    ],
					    "url": "http://api.netflix.com/catalog/titles/movies/70111470",
					    "rating": 5.0,
					    "bookmark": [{ id: 432534, time: 65876586 }]
					}
			    ]
			}
        ];

        var results = [];

        return movieLists.
               map(function (movieList) {
                   return movieList.videos.
                          map(function (video) {
                              return video.boxarts.
                                filter(function (boxart) {
                                    return boxart.width === 150;
                                }).
                                map(function (boxart) {
                                    return { id: video.id, title: video.title, url: boxart.url };
                                })
                          }).concatAll()
               }).concatAll();


    }; // Retrieve id, title, and a 150x200 boxart url for every video

    function exercise21() {

        var videos = [
        {
            "id": 70111470,
            "title": "Die Hard",
            "boxart": "http://cdn-0.nflximg.com/images/2891/DieHard.jpg",
            "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
            "rating": 4.0,
        },
        {
            "id": 654356453,
            "title": "Bad Boys",
            "boxart": "http://cdn-0.nflximg.com/images/2891/BadBoys.jpg",
            "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
            "rating": 5.0,
        },
        {
            "id": 65432445,
            "title": "The Chamber",
            "boxart": "http://cdn-0.nflximg.com/images/2891/TheChamber.jpg",
            "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
            "rating": 4.0,
        },
        {
            "id": 675465,
            "title": "Fracture",
            "boxart": "http://cdn-0.nflximg.com/images/2891/Fracture.jpg",
            "uri": "http://api.netflix.com/catalog/titles/movies/70111470",
            "rating": 5.0,
        }
        ];

        var bookmarks = [
            { id: 470, time: 23432 },
            { id: 453, time: 234324 },
            { id: 445, time: 987834 }
        ];
        var counter;
        var videoIdAndBookmarkIdPairs = [];

        for (counter = 0; counter < Math.min(videos.length, bookmarks.length) ; counter++) {
            videoIdAndBookmarkIdPairs.push({ videoId: videos[counter].id, bookmarkId: bookmarks[counter].id });
        }

        return videoIdAndBookmarkIdPairs;
    }; // Combine videos and bookmarks by index

    function exercise24() {

        var movieLists = [
                    {
                        name: "New Releases",
                        videos: [
                            {
                                "id": 70111470,
                                "title": "Die Hard",
                                "boxarts": [
                                    { width: 150, height: 200, url: "http://cdn-0.nflximg.com/images/2891/DieHard150.jpg" },
                                    { width: 200, height: 200, url: "http://cdn-0.nflximg.com/images/2891/DieHard200.jpg" }
                                ],
                                "url": "http://api.netflix.com/catalog/titles/movies/70111470",
                                "rating": 4.0,
                                "interestingMoments": [
                                    { type: "End", time: 213432 },
                                    { type: "Start", time: 64534 },
                                    { type: "Middle", time: 323133 }
                                ]
                            },
                            {
                                "id": 654356453,
                                "title": "Bad Boys",
                                "boxarts": [
                                    { width: 200, height: 200, url: "http://cdn-0.nflximg.com/images/2891/BadBoys200.jpg" },
                                    { width: 140, height: 200, url: "http://cdn-0.nflximg.com/images/2891/BadBoys140.jpg" }

                                ],
                                "url": "http://api.netflix.com/catalog/titles/movies/70111470",
                                "rating": 5.0,
                                "interestingMoments": [
                                    { type: "End", time: 54654754 },
                                    { type: "Start", time: 43524243 },
                                    { type: "Middle", time: 6575665 }
                                ]
                            }
                        ]
                    },
                    {
                        name: "Instant Queue",
                        videos: [
                            {
                                "id": 65432445,
                                "title": "The Chamber",
                                "boxarts": [
                                    { width: 130, height: 200, url: "http://cdn-0.nflximg.com/images/2891/TheChamber130.jpg" },
                                    { width: 200, height: 200, url: "http://cdn-0.nflximg.com/images/2891/TheChamber200.jpg" }
                                ],
                                "url": "http://api.netflix.com/catalog/titles/movies/70111470",
                                "rating": 4.0,
                                "interestingMoments": [
                                    { type: "End", time: 132423 },
                                    { type: "Start", time: 54637425 },
                                    { type: "Middle", time: 3452343 }
                                ]
                            },
                            {
                                "id": 675465,
                                "title": "Fracture",
                                "boxarts": [
                                    { width: 200, height: 200, url: "http://cdn-0.nflximg.com/images/2891/Fracture200.jpg" },
                                    { width: 120, height: 200, url: "http://cdn-0.nflximg.com/images/2891/Fracture120.jpg" },
                                    { width: 300, height: 200, url: "http://cdn-0.nflximg.com/images/2891/Fracture300.jpg" }
                                ],
                                "url": "http://api.netflix.com/catalog/titles/movies/70111470",
                                "rating": 5.0,
                                "interestingMoments": [
                                    { type: "End", time: 45632456 },
                                    { type: "Start", time: 234534 },
                                    { type: "Middle", time: 3453434 }
                                ]
                            }
                        ]
                    }
        ];
    }; // Retrieve each video's id, title, middle interesting moment time, and smallest box art url.

    function getNgTableConfigurationOptions() {

        // All inital values
        var baseParameters = {
            count: 10,
            sorting: { extn: "asc" },
            //filter: { name: "Angelica"},
            page: 1
        };

        var baseSettings = {
            $loading: true,
            counts: [10, 15, 20, 25],   // page size buttons (right set of buttons in demo)
            dataset: null,
            dataOptions: { //// Dont know about the usage !!!!!!!!!!!!!!!!!!!s
                applyFilter: true,
                applyPaging: true,
                applySort: true
            },
            defaultSort: "desc",
            filterOptions: {
                filterComparator: undefined, // look for a substring match in case insensitive way
                filterDelay: 500,
                filterDelayThreshold: 10000, // size of dataset array that will trigger the filterDelay being applied
                filterFilterName: undefined, // when defined overrides ngTableDefaultGetDataProvider.filterFilterName
                filterFn: undefined, // when defined overrides the filter function that ngTableDefaultGetData uses
                filterLayout: "stack"
            }, //Need to learn more
            getData: function (params) {
                return $http.post('/getDataTableJson')
                            .then(function (response) {

                                var responseData = response.data.employees;

                                var orderedData = params.sorting() ? $filter('orderBy')(responseData, params.orderBy()) : data; //Apply Sorting
                                orderedData = $filter('filter')(orderedData, params.filter()); // Apply Filtering
                                params.total(orderedData.length);

                                return (orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count())); //Apply Paging

                            });
            },
            getGroups: {},
            groupOptions: {},
            /**
             * The collection of interceptors that should apply to the results of a call to
             * the `getData` function before the data rows are displayed in the table
             */
            interceptors: [],
            paginationMaxBlocks: 13, // determines the pager buttons (left set of buttons in demo)
            paginationMinBlocks: 2,
            sortingIndicator: "span", //The html tag that will be used to display the sorting indicator in the table header
            total: 0
        };

        return new NgTableParams(baseParameters, baseSettings);
    };
};

