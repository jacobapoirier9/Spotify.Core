/// <reference path="router.js" />
/// <reference path="../lib/jquery/dist/jquery.js" />

var spotifyEnhancer = {
    home: {
        playlists: {
            init: function () {
                $(".spotify-playlist").on("click", function () {

                    window.location.assign(router.action("Home", "Playlists", { playlistId: $(this).attr("data-playlist-id") }))

                })
            }
        }
    }
}