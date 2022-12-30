$(document).ready(function () {
    $('.spoiler-title').click(function (event) {
        if (!lock) {
            if (!$(this).hasClass("view")) {
                if ($('.spoiler').hasClass('one')) {
                    $('.spoiler-title').not($(this)).removeClass('active');
                    $('.spoiler-text').not($(this).next()).slideUp(300);
                }
                $(this).toggleClass('active').next().slideToggle(300);
            }
        }
        lock = false;
    });
});

$("#save-btn").click(async function (event) {
    const response = await fetch(`/api/service/items`, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(
            {
                header: document.getElementById("serviceHeader").value,
                description: document.getElementById("serviceDescription").value,
            },
        )
    });

    await response.text();

    if (response.ok) {
        window.location.href = "/api/service/items/view";
    }
    console.log(response)
});

async function DeleteService(id) {
    const response = await fetch(`/api/service/items/${id}`, {
        method: "DELETE",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });

    await response.text();

    if (response.ok) {
        window.location.href = "/api/service/items/view";
    }

    console.log(response)
}

async function UpdateService(id) {
    const response = await fetch(`/api/service/items`, {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(
            {
                id: id,
                header: document.getElementById(`serviceHeader-${id}`).value,
                description: document.getElementById(`serviceDescription-${id}`).value,
            },
        )
    });

    await response.text();

    if (response.ok) {
        window.location.href = "/api/service/items/view";
    }

    console.log(response)
}

$(".header-btn").click(function (event) {
    $(`.service-item`)[0].classList.remove('not-visible');
    $(this)[0].classList.add('not-visible');
});

$("#close-btn").click(function (event) {
    $(`.service-item`)[0].classList.add('not-visible');
    $(".header-btn")[0].classList.remove('not-visible');
});

function Edit(id) {
    var header = $(`.service-${id}`).children();
    var description = $(`.service-text-${id}`).children();

    header[0].classList.toggle("not-visible");
    header[1].classList.toggle("not-visible");

    description[0].classList.toggle("not-visible");
    description[1].classList.toggle("not-visible");

    $(".help-menu-service").each(function (i) {

        if (i > 0) {

            var name = $(this).attr("name");

            if (name == id) {
                $(this).children()[0].classList.remove('not-visible');
            }

            $(this).children()[1].classList.add('not-visible');
            $(this).children()[2].classList.add('not-visible');
        }
    });
};

var lock = false;
function Lock() {
    lock = true;
}