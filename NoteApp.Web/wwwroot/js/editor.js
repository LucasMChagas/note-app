function execCmd(command, value = null) {
    document.execCommand(command, false, value);
}

function getEditorContent() {
    return document.querySelector(".editor").textContent;
}

function setEditorContent(content) {
    document.querySelector(".editor").textContent = content;
}

function setCursorToEnd(element) {
    if (!element) return;

    const range = document.createRange();
    const selection = window.getSelection();

    // Coloca o cursor no final do conteúdo
    range.selectNodeContents(element);
    range.collapse(false);

    selection.removeAllRanges();
    selection.addRange(range);

    // Foca novamente no elemento
    element.focus();
}