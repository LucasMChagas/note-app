const noteCard = document.getElementsByClassName("note-card");

noteCard.addEventListener("click", (e) => {
    console.log("clicked");
    noteCard.ClassName = "is-active";
})