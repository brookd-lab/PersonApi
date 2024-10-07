window.addEventListener("load", (event) => {
    setTimeout(() => {
        var lockedIcon = document.getElementById("locked");
        var unlockedIcon = document.getElementById("unlocked");
        lockedIcon.id = "unlocked";
        unlockedIcon.id = "locked";
    }, 100);
});