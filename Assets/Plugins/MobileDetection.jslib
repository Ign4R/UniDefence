mergeInto(LibraryManager.library, {
    isMobile: function() {
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            return 1;
        } else {
            return 0;
        }
    }
});
