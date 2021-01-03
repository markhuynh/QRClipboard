"use strict";

var clipboardContent = document.getElementById("clipboardContent");
var copyButton = document.getElementById("copyButton");

var textInput = document.getElementById("textInput");
var sendButton = document.getElementById("sendButton");
var clearButton = document.getElementById("clearButton");

copyButton.addEventListener("click", event => {
    var r = document.createRange();
    r.selectNode(clipboardContent);
    window.getSelection().removeAllRanges();
    window.getSelection().addRange(r);
    document.execCommand('copy');
    window.getSelection().removeAllRanges();
});

function initHub(groupId) {
    var connection = new signalR.HubConnectionBuilder().withUrl("/clipboardhub").build();

    textInput.addEventListener("keyup", event => {
        if (event.key !== "Enter") return;

        sendButton.click();
        event.target.value = "";
        event.preventDefault();
    });

    sendButton.addEventListener("click", event => {
        var message = textInput.value.trim();
        if (message !== "") {
            connection.invoke("SendMessage", groupId, message).catch(err => {
                return console.error(err.toString());
            });
        }
        event.preventDefault();
    });

    clearButton.addEventListener("click", event => {
        connection.invoke("SendMessage", groupId, "").catch(err => {
            return console.error(err.toString());
        });
        event.preventDefault();
    });

    connection.on("ReceiveMessage", message => {
        clipboardContent.innerText = message;
        if (message === "") {
            copyButton.classList.add("d-none");
        } else {
            copyButton.classList.remove("d-none");
        }
    });

    connection.start().then(function () {
        connection.invoke("JoinGroup", groupId).then(function () {
            sendButton.disabled = false;
            clearButton.disabled = false;
        }
        ).catch(err => {
            console.log(err);
        });
    }).catch(err => {
        return console.error(err.toString());
    });
}