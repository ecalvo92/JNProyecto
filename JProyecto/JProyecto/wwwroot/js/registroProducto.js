function permitirSoloNumeros() {
  $(this).on('keypress', function (e) {
    const code = e.which || e.keyCode;
    if (code < 48 || code > 57) {
      e.preventDefault();
    }
  });
}