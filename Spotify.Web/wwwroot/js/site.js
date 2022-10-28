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
                $.each($(".spotify-interval"), function (index, element) {

                    // Populate progress bar
                    var $element = $(element)
                    var max = $element.data("max")

                    var startMs = $element.data("start")
                    var endMs = $element.data("end")

                    var startPercentage = startMs / max * 100
                    var endPercentage = endMs / max * 100

                    $element.find(".offsetProgress").css("width", startPercentage + "%")
                    $element.find(".actualProgress").css("width", endPercentage + "%")

                    $element.find("a").on("click", function (click) {
                        $.ajax({
                            url: router.action("Home", "SeekTrackPosition"),
                            method: "POST",
                            data: { 
                                trackId: $("#trackId").val(),
                                startMs: startMs
                            }
                        })
                    })
                })
            }
        }
    }
}