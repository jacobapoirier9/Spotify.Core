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
            loadTrackProgressBar: function (positionMs, durationMs) {
                var $trackProgressBar = $("#trackProgressBar")
                var percentComplete = positionMs / durationMs * 100

                $trackProgressBar.data("progress", positionMs)
                $trackProgressBar.css("width", percentComplete + "%")
            },
            init: function () {

                // Keep the track progress bar up to date
                var $trackProgressBar = $("#trackProgressBar")
                if ( $trackProgressBar.length > 0) {
                    var durationMs = $trackProgressBar.data("total-duration")
                    var positionMs = $trackProgressBar.data("progress")

                    spotifyEnhancer.home.singleTrack.loadTrackProgressBar(positionMs, durationMs)

                    var tickInterval = 1000
                    var id = setInterval(function () {
                        positionMs += tickInterval
                        spotifyEnhancer.home.singleTrack.loadTrackProgressBar(positionMs, durationMs)

                        if (positionMs >= durationMs) {
                            $trackProgressBar.closest(".row").hide();
                            clearInterval(id)
                        }
                    }, tickInterval)
                }

                // Bind click events for interval rows
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

                    $element.find("#playSection").on("click", function (click) {
                        $.ajax({
                            url: router.action("Home", "SeekTrackPosition"),
                            method: "POST",
                            data: { 
                                trackId: $("#trackId").val(),
                                startMs: startMs
                            },
                            success: function () {
                                window.location.reload()
                            }
                        })
                    })
                })

                $("#btnPlayTrack").on("click", function (click) {
                    $.ajax({
                        url: router.action("Home", "PlayTrack"),
                        method: "POST",
                        data: {
                            trackId: $("#trackId").val()
                        },
                        success: function () {
                            window.location.reload()
                        }
                    })
                })
            }
        }
    }
}