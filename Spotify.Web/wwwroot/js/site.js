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
        }
    }
}