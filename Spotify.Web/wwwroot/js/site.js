/// <reference path="router.js" />
/// <reference path="../lib/jquery/dist/jquery.js" />

var spotifyEnhancer = {
    home: {
        multiplePlaylists: {
            init: function () {
                $(".spotify-playlist").on("click", function () {

                    window.location.assign(router.action("Home", "Playlists", { playlistId: $(this).attr("data-playlist-id") }))

                })
            }
        },
        singlePlaylist: {
            init: function () {
                $(".spotify-track").on("click", function () {

                    window.location.assign(router.action("Home", "Tracks", { trackId: $(this).attr("data-track-id") }))

                })
            }
        },
        singleTrack: {
            init: function () {
                //$("#links .nav-link").on("show.bs.tab", function (show) {
                //    console.debug(show)
                //})

                var $offsetProgress = $("#offsetProgress")
                var $actualProgress = $("#actualProgress")

                $.each($(".spotify-interval"), function (index, element) {
                    var $element = $(element)
                    var max = $element.data("max")

                    var start = $element.data("start") / max * 100
                    var end = $element.data("end") / max * 100

                    console.debug(start, end, max)

                    $element.find(".offsetProgress").css("width", start + "%")
                    $element.find(".actualProgress").css("width", end + "%")
                })
            }
        }
    }
}