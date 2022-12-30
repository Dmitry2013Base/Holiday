var item = document.querySelectorAll(".list-item")

if (item.length < 11) {
    document.querySelector(".list-view-btn").classList.add("list-item-not-visible")
}

for (let i = 10; i < item.length; i++) {
    item[i].classList.add("list-item-not-visible")
}

function Show() {
    var notVisibleItem = document.querySelectorAll(".list-item-not-visible")
    var count = notVisibleItem.length

    try {
        for (let i = 0; i < 2; i++) {
            notVisibleItem[i].classList.remove("list-item-not-visible")
        }
    } catch (error) {

    }

    if (count - 2 <= 0) {
        document.querySelector(".list-view").classList.add("list-item-not-visible")
    }
}

var id = "";
var view = document.querySelector(".role");
var checkedBoxes = document.querySelectorAll('input[type=checkbox]');
document.querySelectorAll(".view-role").forEach(function (r) {
    r.addEventListener("click", async function (e) {

        var userName = document.querySelector("#user-name");
        userName.innerHTML = "";
        userName.innerHTML = e.target.closest('div').querySelector(".user-name").textContent;
        id = e.target.closest('div').querySelector(".id").textContent;

        const getRoles = await fetch(`/roles/GetUserRoles/${id}`, {
            method: "GET",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
        });

        var roles = await getRoles.json();
        
        checkedBoxes.forEach(e => {

            if (roles.includes(e.nextSibling.textContent.replace(/\s/g, ""))) {

                if (!e.hasAttribute("checked")) {
                    e.setAttribute("checked", "checked");
                }
            }
            else {

                if (e.hasAttribute("checked")) {
                    e.removeAttribute("checked");
                }
            }
        });

        view.classList.remove("none");
        view.style.left = window.innerWidth / 2 - view.getBoundingClientRect().width / 2 + 'px';

        if (e.pageY + view.getBoundingClientRect().height > window.innerHeight) {
            var start = e.pageY - view.getBoundingClientRect().height - 20;
            view.style.top = start + 'px';

            if (start < 0) {
                view.style.top = Math.abs(start) + "px";
            }
        } else {
            view.style.top = e.pageY + 20 + 'px';
        }
    })
})

document.getElementById("close").addEventListener("click", async function (e) {

    const listRoles = [];

    document.querySelectorAll('input[type=checkbox]:checked').forEach(i => {
        listRoles.push(i.nextSibling.textContent.replace(/\s/g, ""));
    });

    const response = await fetch(`/roles/SaveUserRoles/${id}`, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(
            {
                listRoles,
            },
        )
    });

    view.classList.add("none")
    window.location.href = `/Roles/Index`;
})

document.querySelectorAll(".del").forEach(i => {

    i.addEventListener("click", async function (e) {

    var userIdFull = e.target.closest('div').querySelector(".id").textContent;

    const response = await fetch(`DeleteUser/${userIdFull}`, {
        method: "DELETE",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });

    var check = await response.text();

    if (check === "true") {
        window.location.href = "/Roles/Index";
    }

    console.log(response)
    })
});

