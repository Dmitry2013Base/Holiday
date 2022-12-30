async function DeleteProject(id) {

    const response = await fetch(`/api/project/items/${id}`, {
        method: "DELETE",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });

    await response.text();

    if (response.ok) {
        window.location.href = "/api/project/items/view";
    }

    console.log(response)
}

function RedirectEditView(id) {
    var url = "/api/project/edit/" + id + "/true";
    window.location.href = url;
}

function RedirectEdit(id) {
    var url = "/api/project/edit/" + id
    window.location.href = url;
}

var checkImage = false;
var checkAll = false;
function ChangeImage() {
    var file = document.getElementById('file').files[0];
    var reader = new FileReader();
    reader.onload = function (e) {
        var image = document.createElement("img");
        image.src = e.target.result;
        image.id = "projectImage"

        var img = document.getElementById('projectImage');
        if (img != null) {
            img.remove();
        }
        document.querySelector(".item-image").appendChild(image);
    }
    reader.readAsDataURL(file);

    checkImage = true;
    CheckValid();
}

async function SubmitCreate() {
    const response = await fetch(`/api/project/items`, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(
            {
                header: document.getElementById("projectHeader").value,
                description: document.getElementById("projectDescription").value,
                imageProject: document.getElementById("projectImage").src
            },
        )
    });

    await response.text();

    if (response.ok) {
        window.location.href = "/api/project/items/view";
    }
    console.log(response)
}

async function SubmitUpdate(id) {
    const response = await fetch(`/api/project/items`, {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(
            {
                id: id,
                header: document.getElementById("projectHeader").value,
                description: document.getElementById("projectDescription").value,
                imageProject: document.getElementById("projectImage").src
            },
        )
    });

    await response.text();

    if (response.ok) {
        window.location.href = "/api/project/items/view";
    }

    console.log(response)
}

function CheckValid() {

    var header = document.getElementById("projectHeader").value;
    var description = document.getElementById("projectDescription").value;
    var sub = document.querySelector("#sub");

    if (checkImage == true) {
        if (header != "" && description != "") {
            checkAll = true;
        } else {
            checkAll = false;
        }
    } else {

        if (document.querySelector(".item-image").childElementCount != 0) {
            checkImage = true;
            CheckValid();
        }
    }

    if (checkAll != false) {
        if (sub.hasAttribute("disabled")) {
            sub.removeAttribute("disabled")
        }    
    } else {
        if (!sub.hasAttribute("disabled")) {
            sub.setAttribute("disabled", true)
        } 
    }
}
