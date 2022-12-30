var createItem = document.querySelector(".section-create");
var btnEdit = document.querySelector(".edit-btn");
var btnCreate = document.querySelector(".create-btn");


window.onload = async function () {
    const response = await fetch(`/api/contact/items/social`, {
        method: "GET",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });

    var listSocial = await response.json();

    for (var i = 0; i < listSocial.length; i++) {

        document.querySelector(".social").innerHTML +=

            `<div class="contact-description flex">
                <p>
                    <a href="${listSocial[i].value}" target="_blank">
                        ${listSocial[i].header}
                    </a>
                </p>
                <label class="item-id none">${listSocial[i].id}</label>
                <div class="help-menu none flex">
                    <button class="edit icon-help flex">
                        <svg viewBox="0 0 128 128"><path d="M123.315,16L112.003,4.686C108.878,1.563,104.78,0,100.687,0s-8.188,1.563-11.313,4.686l-68.69,68.689  C17.563,76.5,8.004,88.586,8,92.68L0,128l35.313-8c0,0,16.188-9.563,19.313-12.688l68.69-68.687  C129.562,32.375,129.562,22.243,123.315,16z M10.605,117.398l5.195-22.953c0.074-0.328,0.129-0.664,0.16-0.992  c0.016-0.047,0.059-0.117,0.078-0.164l18.09,18.094c-0.605,0.367-1.215,0.734-1.813,1.094L10.605,117.398z M48.984,101.641  c-0.906,0.859-4.039,2.977-7.867,5.414L20.391,86.328c2.125-2.914,4.492-5.844,5.949-7.297l51.722-51.718l22.625,22.625  L48.984,101.641z M117.659,32.969l-11.316,11.313L83.718,21.657l11.316-11.313C96.542,8.829,98.55,8,100.687,8  s4.148,0.836,5.66,2.344l11.313,11.313c1.512,1.508,2.34,3.516,2.34,5.656C119.999,29.446,119.167,31.461,117.659,32.969z" fill="black"/></svg>
                    </button>
                    <button class="icon-help flex">
                        <svg viewBox="0 0 32 32"><path d="M4,29a1,1,0,0,1-.71-.29,1,1,0,0,1,0-1.42l24-24a1,1,0,1,1,1.42,1.42l-24,24A1,1,0,0,1,4,29Z"/><path d="M28,29a1,1,0,0,1-.71-.29l-24-24A1,1,0,0,1,4.71,3.29l24,24a1,1,0,0,1,0,1.42A1,1,0,0,1,28,29Z"/></svg>
                    </button>
                </div>
            </div>`;
    }
};

btnCreate.addEventListener("click", async function () {
    if (CheckEmpry()) {
        var create = document.querySelectorAll(".create-input");
        var tag = -1;

        if (create[0].value.includes("svg")) {
            tag = 3;
        }
        else if (create[1].value.includes("@")) {
            tag = 2;
        }
        else {

            if (create[1].value.includes("89") || create[1].value.includes("+79")) {
                tag = 1;
            }
            else {
                tag = 0;
            }
        }

        const response = await fetch(`/api/contact/items`, {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify(
                {
                    header: create[0].value,
                    value: create[1].value,
                    tag: tag,
                },
            )
        });

        await response.text();

        if (response.ok) {
            window.location.href = "/api/contact/items/view";
        }
        console.log(response)
    }
})

document.querySelectorAll(".delete").forEach(function (e) {
    e.addEventListener("click", async function (i) {

        var itemId = i.target.closest('.contact-description').querySelector(".item-id").textContent;

        const response = await fetch(`/api/contact/items/${itemId}`, {
            method: "DELETE",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
        });

        var check = await response.text();

        if (response.ok) {
            window.location.href = "/api/contact/items/view";
        }

        console.log(response)
    })
});

btnEdit.addEventListener("click", async function () {
    if (CheckEmpry()) {
        var create = document.querySelectorAll(".create-input");
        var itemId = document.querySelector(".contact-id").value;

        var tag = -1;

        if (create[0].value.includes("svg")) {
            tag = 3;
        }
        else if (create[1].value.includes("@")) {
            tag = 2;
        }
        else {

            if (create[1].value.includes("89") || create[1].value.includes("+79")) {
                tag = 1;
            }
            else {
                tag = 0;
            }
        }

        const response = await fetch(`/api/contact/items`, {
            method: "PUT",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify(
                {
                    id: itemId,
                    header: create[0].value,
                    value: create[1].value,
                    tag: tag,
                },
            )
        });

        await response.text();

        if (response.ok) {
            window.location.href = "/api/contact/items/view";
        }

        console.log(response)
    }
})

document.querySelectorAll(".create-icon").forEach(function (e) {
    e.addEventListener("click", function (i) {

        var create = document.querySelectorAll(".create-input");
        create[0].value = "";
        create[1].value = "";

        btnCreate.classList.remove("none")
        createItem.classList.remove("none");
    })
});

document.querySelectorAll(".edit").forEach(function (e) {
    e.addEventListener("click", async function (i) {

        var itemId = i.target.closest('.contact-description').querySelector(".item-id").textContent;
        var create = document.querySelectorAll(".create-input");
        
        const response = await fetch(`/api/contact/items/${itemId}`, {
            method: "GET",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
        });

        var check = await response.json();

        document.querySelector(".contact-id").value = check.id;
        create[0].value = check.header;
        create[1].value = check.value;

        btnEdit.classList.remove("none");
        createItem.classList.remove("none")
    })
});

document.querySelector(".create-close").addEventListener("click", function () {
    createItem.classList.add("none");

    if (!btnEdit.classList.contains("none")) {
        btnEdit.classList.add("none");
    }
    if (!btnCreate.classList.contains("none")) {
        btnCreate.classList.add("none");
    }
});

function CheckEmpry() {

    var create = document.querySelectorAll(".create-input");
    var check = [];

    create.forEach(function (e) {

        if (e.value != "") {
            check.push(e.value);
        }
    })

    if (check.length == create.length) {
        return true;
    }
    else {
        return false;
    }
}
