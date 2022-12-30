var globalEditBtn = document.querySelector(".global-edit-btn");
var globalSaveBtn = document.querySelector(".global-save-btn");
var helpMenu = document.querySelectorAll(".help");
const elements = [];
const newElements = [];

GetContact();

document.querySelectorAll(".ui-section").forEach(async function (e) {
    var key = e.getAttribute("id"); 

    const response = await fetch(`/api/ui/items/${key}`, {
        method: "GET",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });

    var list = await response.json();

    for (let i = 0; i < list.length; i++) {
        var el = document.querySelector(`#${list[i].key}`);

        if (el != null) {
            el.innerHTML = list[i].value;
            elements.push(el);
        }
    }
});

globalEditBtn.addEventListener("click", function (e) {

    globalSaveBtn.classList.remove("none");
    e.target.setAttribute("disabled", true)

    elements.forEach(function (i) {
        var key = i.getAttribute("id");
        var parent = i.parentNode;


        var input = document.createElement("input")
        input.setAttribute("id", key)
        input.classList.add("global-edit-input");
        input.value = i.innerHTML.trim();

        parent.removeChild(i);
        parent.append(input);
    });

    helpMenu.forEach(function (i) {
        i.classList.add("none");
    });

    var sw = document.querySelector(".edit-sw");

    if (sw != null) {

        sw.classList.remove("none");
    }
})

globalSaveBtn.addEventListener("click", async function (e) {

    newElements.splice(0, newElements.length)

    elements.forEach(function (i) {

        var key = i.getAttribute("id");
        var value = document.querySelector(`#${key}`).value;

        newElements.push({ key: key, value: value })
    });

    const response = await fetch(`/api/ui/items`, {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(
            {
                newElements,
            },
        )
    });

    await response.text();

    if (response.ok) {

        e.target.classList.add("none");
        globalEditBtn.removeAttribute("disabled")

        elements.forEach(function (i) {

            var key = i.getAttribute("id");
            var el = document.querySelector(`#${key}`);
            var parent = el.parentNode;
            var value = el.value;
            var newElement = i;

            parent.removeChild(el);
            newElement.innerHTML = value;
            parent.append(newElement)
        });

        helpMenu.forEach(function (i) {
            i.classList.remove("none");
        });

        var sw = document.querySelector(".edit-sw");

        if (sw != null) {

            document.querySelector(".edit-sw").classList.add("none");
        }
    }

    console.log(response)
});

async function GetContact() {

    const getcont = await fetch(`/api/contact/items/tel`, {
        method: "GET",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });

    var contacts = await getcont.json();
    var nav = document.querySelector(".footer-nav");

    contacts.forEach(function (i) {

        nav.innerHTML +=
            `<li class="footer-item col-3">
                <a href="tel:${i.value}" class="footer-item-link">${i.header}</a>
            </li>`
    });
};