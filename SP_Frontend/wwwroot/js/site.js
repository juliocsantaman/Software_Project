// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
console.log("Running...");
const urlBase = "https://localhost:7130";
const inputWord = document.getElementById("inputWord");
const btnSearch = document.getElementById("btnSearch");
const container = document.getElementById("container");
let urlContainer;
let p;




async function fetchUrls() {
    urlContainer = document.createElement("div");
    p = document.createElement("p");

    container.appendChild(urlContainer);
    urlContainer.id = "urlContainer";
    urlContainer.className = "url-container";

    if (inputWord.value.trim().length >= 1) {
        p.innerHTML = "<strong>Loading...</strong>";
        urlContainer.appendChild(p);

        const response = await fetch(`${urlBase}/api/Dictionary/${inputWord.value}`);
        const urls = await response.json();
        return urls;
    }

    p.innerHTML = "<strong>Ingresa una palabra</strong>";
    urlContainer.appendChild(p);
   
}

function drawElements() {
    if (container.contains(document.getElementById("urlContainer"))) {
        container.removeChild(document.getElementById("urlContainer"));
    }

    let urlList = [];

    fetchUrls().then((data) => {
        if (data.urlList == null) {
            urlContainer.removeChild(p);
            let h3 = document.createElement("h3");
            h3.innerHTML = `<strong>No se encuentra la palabra: <span class="input-word">${inputWord.value}</span> en nuestro diccionario</strong>`;
            inputWord.value = "";
            urlContainer.appendChild(h3);
            return;
        }

        urlContainer.removeChild(p);
        urlList = data.urlList;
        //console.log(urlList);
        let h3 = document.createElement("h3");
        h3.innerHTML = `<strong>Results of: <span class="input-word">${inputWord.value}</span> </strong>`;
        urlContainer.appendChild(h3);

        for (let i = 0; i < urlList.length; i++) {
            let href = urlList[i].replace("html-ordered.txt", "html");
            //let timeMs = href.slice(href.indexOf("?"));
            //timeMs = timeMs.slice(timeMs.indexOf("=")+1);
            //console.log(timeMs);
            let a = document.createElement("a");

            a.setAttribute("href", href + "ms");
            a.setAttribute("target", "_blank");
            a.innerHTML = href + "ms";

            urlContainer.appendChild(a);
        }

        inputWord.value = "";
    });
}

inputWord.addEventListener("keyup", (event) => {
    if (event.keyCode == 13) {
        drawElements();
    }
});

btnSearch.addEventListener("click", () => {
    drawElements();
});

//btnSearch.addEventListener("click", () => {
//    let urlList = [];

//    fetch(`${urlBase}/api/Dictionary/${inputWord.value}`)
//        .then((response) => {
//            return response.json();
//        })
//        .then((data) => {
//            urlList = data.urlList;
//            let h3 = document.createElement("h3");
//            h3.innerHTML = "Results of: " + inputWord.value;
//            urlContainer.appendChild(h3);

//            for (let i = 0; i < urlList.length; i++) {
//                let href = urlList[i].replace("html-ordered.txt", "html");
//                let a = document.createElement("a");

//                a.setAttribute("href", href);
//                a.setAttribute("target", "_blank");
//                a.innerHTML = href;

//                urlContainer.appendChild(a);
//            }
//        });

//});

