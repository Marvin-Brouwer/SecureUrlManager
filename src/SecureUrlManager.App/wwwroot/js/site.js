import toastify from '/node_modules/toastify-js/src/toastify-es.js';

export function copyHref(href) {
    var toast = toastify({
        text: `Copied "${href}" to clipboard`,
        duration: 3000,
        style: {
            background: "unset",
            background: "top",
        },
    });
    navigator.clipboard.writeText(href)
        .catch(() => {
            var inp = document.createElement('input');
            document.body.appendChild(inp)
            inp.value = href
            inp.select();
            document.execCommand('copy', false);
            inp.remove();
        })
        .then(() => toast.showToast())
        .catch((e) => {
            prompt("Copy failed, please copy the prompt value", href);
        })
}

window.copyHref = copyHref;