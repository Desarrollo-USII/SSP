document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('formEncriptar');
    
    if (form) {
        form.addEventListener('submit', async function (e) {
            e.preventDefault(); // Evita recargar la página
            
            const texto = document.getElementById('txtEncriptar').value;
            const btn = document.getElementById('btnEncriptar');
            const resultadoDiv = document.getElementById('divResultadoEncriptar');
            
            // Leemos la URL segura directamente desde el HTML
            const urlAjax = form.getAttribute('data-url');

            // Estado de carga
            btn.disabled = true;
            btn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span> Procesando...';

            try {
                // Petición moderna con async/await
                const response = await fetch(urlAjax, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    body: new URLSearchParams({ 'texto': texto })
                });

                const data = await response.json();

                // Mostramos el resultado
                if (data.success) {
                    resultadoDiv.innerHTML = `
                        <div class="alert alert-success p-2 small border-0 shadow-sm">
                            <span class="fw-bold">Hash generado:</span><br/>
                            <span class="user-select-all font-monospace">${data.resultado}</span>
                        </div>`;
                } else {
                    resultadoDiv.innerHTML = `<div class="alert alert-danger p-2 small border-0 shadow-sm">${data.mensaje}</div>`;
                }

            } catch (error) {
                resultadoDiv.innerHTML = `<div class="alert alert-danger p-2 small border-0 shadow-sm">Error de conexión con el servidor.</div>`;
            } finally {
                // Restauramos el botón sin importar si hubo éxito o error (finally)
                btn.disabled = false;
                btn.innerHTML = '<i class="fa-solid fa-lock me-2"></i> Encriptar';
            }
        });
    }
});