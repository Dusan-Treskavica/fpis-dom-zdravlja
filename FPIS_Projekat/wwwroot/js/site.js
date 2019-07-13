function formatirajDatumVremeEur(datumVreme) {
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var dan = (datumVreme.getDate() < 10) ? "0" + datumVreme.getDate() : datumVreme.getDate();
    var mesec = monthNames[datumVreme.getMonth()];
    var godina = datumVreme.getFullYear();
    var sati = datumVreme.getHours();
    var minuti = (datumVreme.getMinutes() < 10) ? "0" + datumVreme.getMinutes() : datumVreme.getMinutes();
    return dan + "-" + mesec + "-" + godina + " " + sati + ":" + minuti;
}

function formatirajDatumUSA(datum) {
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var dan = (datum.getDate() < 10) ? "0" + datum.getDate() : datum.getDate();
    //var mesec = monthNames[datum.getMonth()];
    var mesec = (datum.getMonth() < 10) ? "0" + datum.getMonth() : datum.getMonth();
    var godina = datum.getFullYear();
    return godina + "-" + mesec + "-" + dan;
}

$(document).ready(function () {
    //sakrijPoruku();
});

function prikaziPoruku(poruka) {
    sakrijPoruku();
    $('#poruka-info').children('.poruka').html(poruka);
    $('#poruka-info').show();
    $('#poruka-info').focus();
}

function sakrijPoruku() {
    $('#poruka-info').hide();
}

$(document).ready(function () {
    $("#table_source").on("click", "td.move-row", function () {
        var tr = $(this).closest("tr").remove().clone();
        var index = tr.children('input[name="TerminiTerapije.Index"]').attr('value');
        var statusiTermina = '<select id="statusTermina" name="OdabraniTermini[' + index + '].Status" class="form-control">' +
            '<option value="Zakazan" selected>Zakazan</option>' +
            '<option value="Otkazan">Otkazan</option>' +
            '<option value="Izvrsen">Izvrsen</option>' +
            '</select>';

        tr.find('[name*="TerminiTerapije"]').each(function () {
            $(this).attr('name', $(this).attr('name').replace('TerminiTerapije', 'KarticaZaEvidencijuDTO.OdabraniTermini'));
        });

        tr.children('.statusTermina').html(statusiTermina);
        tr.children('.statusTermina').removeAttr('hidden');

        izborTerminaAjax(tr);

        $("#table_dest tbody").append(tr);
        //sort($("#table_dest"));
    });

    $("#table_dest").on("click", " td.move-row", function () {

        var tr = $(this).closest("tr").remove().clone();
        tr.find('[name*="OdabraniTermini"]').each(function () {
            $(this).attr('name', $(this).attr('name').replace('KarticaZaEvidencijuDTO.OdabraniTermini', 'TerminiTerapije'));
        });

        tr.children('.statusTermina').attr('hidden', true);

        brisanjeTerminaAjax(tr);

        //TODO: Pre nego sto se premesti termin potrebno je uporediti da li se datumi termina u obe tabele poklapaju

        $("#table_source tbody").append(tr);
        sort($("#table_source"));
    });

    function izborTerminaAjax(tr) {
        var radnikId = tr.find('.radnikId').children('input').val();
        var datumVremeTermina = tr.find('.vremeDatumTerapije').children('input').val();

        $.post("/KarticaEvidencije/DodajTerminNaKarticu", {
            radnikId: radnikId,
            datumVremeTermina: datumVremeTermina
        },
            function (data) {
                console.log(data);
                //TODO: proveri dobijene podatke
            }
        );
    }

    function brisanjeTerminaAjax(tr) {
        var radnikId = tr.find('.radnikId').children('input').val();
        var datumVremeTermina = tr.find('.vremeDatumTerapije').children('input').val();

        $.post("/KarticaEvidencije/ObrisiTerminSaKartice", {
            radnikId: radnikId,
            datumVremeTermina: datumVremeTermina
        },
            function (data) {
                console.log(data);
                //TODO: proveri dobijene podatke
            }
        );
    }
});

function sort(table) {
    var rows = table.find('tr:gt(0)').toArray().sort(comparer(0));

    for (var i = 0; i < rows.length; i++) {
        table.append(rows[i]);
    }
}

function comparer(index) {
    return function (a, b) {
        var valA = getCellValue(a, index);
        var valB = getCellValue(b, index);
        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.toString().localeCompare(valB);
    };
}

function getCellValue(row, index) {
    //var value = $(row).children('td').children('label').eq(index).text();
    var value = $(row).children('input').val();

    return value;
}

$('#kreiraj-karticu').on({
    'click': function (e) {
        e.preventDefault();
        sakrijPoruku();

        var brojUputa = $('#KarticaZaEvidencijuDTO_BrojUputa').val().trim();
        $.post('/KarticaEvidencije/NovaKartica', {
            brojUputa: brojUputa
        },
            function (data) {
                //TODO: check redirection parameter from data
                if (data !== null) {
                    if (data.errorMessage === null || data.errorMessage === "") {
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_ImePrezime').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.imePrezime);
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_Pol').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.pol);
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_DatumRodjenja').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.datumRodjenja);
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_Telefon').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.telefon);
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_JmbgPacijenta').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.jmbgPacijenta);

                        vratiSifruNoveKartice();
                    } else {
                        prikaziPoruku(data.errorMessage);
                    }
                } else {
                    //window.location.replace("http://stackoverflow.com");
                    //TODO: redirect to error page
                    prikaziPoruku("Greska!!! Uneti uput ne postoji.");
                }
            }
        );
    }
});

$('#nadji-karticu').on({
    'click': function (e) {
        e.preventDefault();
        sakrijPoruku();

        var sifraKartice = $('#KarticaZaEvidencijuDTO_SifraKartice').val().trim();
        $.post('/KarticaEvidencije/PronadjiKarticu', {
            sifraKartice: sifraKartice
        },
            function (data) {
                //TODO: check redirection parameter from data
                if (data !== null) {
                    if (data.errorMessage === null || data.errorMessage === "") {
                        console.log(data);
                        var jsDatum = new Date(data.karticaZaEvidencijuDTO.datumIzdavanja);
                        console.log(jsDatum);

                        $('#KarticaZaEvidencijuDTO_DatumIzdavanja').val(formatirajDatumUSA(jsDatum));
                        $('#KarticaZaEvidencijuDTO_BrojUputa').val(data.karticaZaEvidencijuDTO.brojUputa);
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_ImePrezime').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.imePrezime);
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_Pol').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.pol);
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_DatumRodjenja').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.datumRodjenja);
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_Telefon').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.telefon);
                        $('#KarticaZaEvidencijuDTO_UputZaTerapiju_Pacijent_JmbgPacijenta').val(data.karticaZaEvidencijuDTO.uputZaTerapiju.pacijent.jmbgPacijenta);

                        var uslugeSelect = '<select id="KarticaZaEvidencijuDTO_SifraUsluge" name="KarticaZaEvidencijuDTO.SifraUsluge" class="form-control">';
                        $.each(data.usluge, function (i, usluga) {
                            uslugeSelect += '<option value="' + usluga.value + '">' + usluga.text + '</option>';
                        });
                        uslugeSelect += "</select>"
                        $('#div-usluge').html(uslugeSelect);
                        $('#KarticaZaEvidencijuDTO_SifraUsluge').val(data.karticaZaEvidencijuDTO.sifraUsluge);

                        var statusi = '<select class="form-control">';
                        $.each(data.statusiTermina, function (i, status) {
                            statusi += '<option value="' + status.value + '">' + status.text + '</option>';
                        });
                        statusi += "</select>";

                        var $statusiHtml = $.parseHTML(statusi);
                        console.log($statusiHtml[0].outerHTML);

                        var termini = '';
                        $('#table-odabraniTermini-body').empty();
                        $.each(data.karticaZaEvidencijuDTO.odabraniTermini, function (i, termin) {
                            var jsDatum = new Date(termin.vremeDatumTerapije);
                            $($statusiHtml).attr('name', 'KarticaZaEvidencijuDTO.OdabraniTermini[' + i + '].Status');

                            $($statusiHtml).find('option').filter(function () {
                                console.log($(this).text());
                                return $(this).text() == termin.status;
                            }).attr("selected", "selected");

                            termini += '<tr>' +
                                '<input type="hidden" name="KarticaZaEvidencijuDTO.OdabraniTermini.Index" value="' + i + '" />' +
                                '<td class="radnikId" hidden><input name="KarticaZaEvidencijuDTO.OdabraniTermini[' + i + '].RadnikId" type="text" value="' + termin.radnikId + '"></td > ' +
                                '<td class="fizioterapeut"> <label name="Fizioterapeut">' + termin.imeRadnika + '</label></td>' +
                                '<td class="vremeTerapije"> <label name="VremeTermina">' + formatirajDatumVremeEur(jsDatum) + '</label></td>' +
                                '<td class="vremeDatumTerapije" hidden><input name="KarticaZaEvidencijuDTO.OdabraniTermini[' + i + '].VremeDatumTerapije" type="text" value="' + termin.vremeDatumTerapije + '"></td>' +
                                '<td class="statusTermina">' + $($statusiHtml).prop('outerHTML') + '</td>' +
                                '<td class="btn move-row">Izaberi</td>' +
                                '</tr> ';
                        });
                        $('#table-odabraniTermini-body').html(termini);
                        
                    }
                    else {
                        prikaziPoruku(data.errorMessage);
                    }
                } else {
                    //TODO: redirect to error page
                    prikaziPoruku("Greska !!! Analiza sa unetom sifrom ne postoji.");
                }
            }
        );
    }
});

function vratiSifruNoveKartice() {
    $.post('/KarticaEvidencije/VratiSifruKartice', function (data) {
        //TODO: check redirection parameter from data
        if (data !== null) {
            var jsDate = new Date(data.karticaZaEvidencijuDTO.datumIzdavanja);
            $('#KarticaZaEvidencijuDTO_SifraKartice').val(data.karticaZaEvidencijuDTO.sifraKartice);

            vratiUsluge();
            //TODO: popuni datumKreiranja sa jsDate transformacijom 
        } else {
            prikaziPoruku("Greska!!! Uneti uput ne postoji.");
        }
    });
}

function vratiUsluge() {
    $.get('/api/webservice/usluge', function (data) {
        //TODO: check redirection parameter from data
        if (data !== null) {
            var uslugeSelect = '<select id="KarticaZaEvidencijuDTO_SifraUsluge" name="KarticaZaEvidencijuDTO.SifraUsluge" class="form-control">';
            $.each(data, function (i, usluga) {
                uslugeSelect += '<option value="' + usluga.value + '">' + usluga.text + '</option>';
            });
            uslugeSelect += "</select>";

            $('#div-usluge').html(uslugeSelect);
        }
    });
}

$('#izborUslugeBtn').on({
    'click': function (e) {
        e.preventDefault();
        sakrijPoruku();

        var sifraUsluge = $('#KarticaZaEvidencijuDTO_SifraUsluge').val();
        //alert("sifra usluge: " + uslugaId);
        $.post('/KarticaEvidencije/IzaberiUslugu', {
            sifraUsluge: sifraUsluge
        },
            function (data) {
                console.log(data);
                if (data.errorMessage === null || data.errorMessage === "") {
                    $('#DatumTermina').removeAttr('disabled');
                }
                else {
                    prikaziPoruku(data.errorMessage);
                    $(document).scrollTop(0);
                }
            }
        );
    }
});

$('.statusTermina').on({
    'change': function (e) {
        e.preventDefault();

        var statusTerminaValue = $(this).find('select').val();
        var radnikId = $(this).closest('tr').find('.radnikId').children('input').val();
        var datumVremeTermina = $(this).closest('tr').find('.vremeDatumTerapije').children('input').val();

        $.post('/KarticaEvidencije/PromeniStatusTermina', {
            radnikId: radnikId,
            datumVremeTermina: datumVremeTermina,
            statusTermina: statusTerminaValue
        }, function (data) {
            console.log(data);
            //TODO: proveri dobijene podatke
        });
    }
});

$('#nova-analiza').on({
    'click': function (e) {
        e.preventDefault();
        sakrijPoruku();

        $.post('/Analiza/VratiSifruAnalize', function (data) {
            //TODO: check redirection parameter from data
            if (data !== null) {
                console.log(data.analizaDTO.sifraAnalize);
                $('#AnalizaDTO_SifraAnalize').val(data.analizaDTO.sifraAnalize);

            } else {
                //TODO: redirect to error page
                prikaziPoruku("Greska pri kreiranju nove sifre analize.");
            }
        }
        );
    }
});

$('#nadji-analizu').on({
    'click': function (e) {
        e.preventDefault();
        sakrijPoruku();
        var sifraAnalize = $('#AnalizaDTO_SifraAnalize').val().trim();

        $.post('/Analiza/PronadjiAnalizu', {
            sifraAnalize: sifraAnalize
        },
            function (data) {
                //TODO: check redirection parameter from data
                if (data !== null) {
                    if (data.errorMessage === null || data.errorMessage === "") {
                        var jediniceMereOptions = '';
                        $.each(data.jedinicaMereLista, function (i, jedinicaMere) {
                            //console.log(jedinicaMere.value);
                            jediniceMereOptions += '<option value="' + jedinicaMere.value + '">' + jedinicaMere.text + '</option>';
                        });

                        $('#AnalizaDTO_DonjaGranicaJedinicaMere').html(jediniceMereOptions);
                        $('#AnalizaDTO_GornjaGranicaJedinicaMere').html(jediniceMereOptions);

                        $('#AnalizaDTO_SifraAnalize').val(data.analizaDTO.sifraAnalize);
                        $('#AnalizaDTO_NazivAnalize').val(data.analizaDTO.nazivAnalize);
                        $('#AnalizaDTO_DonjaGranica').val(data.analizaDTO.donjaGranica);
                        $('#AnalizaDTO_DonjaGranicaJedinicaMere').val(data.analizaDTO.donjaGranicaJedinicaMere);
                        $('#AnalizaDTO_GornjaGranica').val(data.analizaDTO.gornjaGranica);
                        $('#AnalizaDTO_GornjaGranicaJedinicaMere').val(data.analizaDTO.gornjaGranicaJedinicaMere);

                    }
                    else {
                        prikaziPoruku(data.errorMessage);
                    }
                } else {
                    //TODO: redirect to error page
                    prikaziPoruku("Greska !!! Analiza sa unetom sifrom ne postoji.");
                }
            }
        );
    }
});

$('#DatumTermina').on({
    'change': function (e) {
        e.preventDefault();

        $datumTermina = $('#DatumTermina').val();
        $usluga = $('#KarticaZaEvidencijuDTO_SifraUsluge :selected').val();

        $.post('/KarticaEvidencije/VratiTermineIFizioterapeute', {
            datumTermina: $datumTermina,
            sifraUsluge: $usluga

        }, function (data) {
            console.log(data);
            if (data !== null && data !== undefined) {

                if (data.errorMessage) {
                    $('#table-termini-body').empty();

                    prikaziPoruku(data.errorMessage);
                    $(document).scrollTop(0);
                } else {
                    var termini = '';
                    $('#table-termini-body').empty();
                    $.each(data, function (i, termin) {
                        var jsDatum = new Date(termin.vremeDatumTerapije);
                        termini += '<tr>' +
                            '<input type="hidden" name="TerminiTerapije.Index" value="' + i + '" />' +
                            '<td class="radnikId" hidden><input name="TerminiTerapije[' + i + '].RadnikId" type="text" value="' + termin.radnikId + '"></td > ' +
                            '<td class="fizioterapeut"> <label name="Fizioterapeut">' + termin.fizioterapeut + '</label></td>' +
                            '<td class="vremeTerapije"> <label name="VremeTermina">' + formatirajDatumVremeEur(jsDatum) + '</label></td>' +
                            '<td class="vremeDatumTerapije" hidden><input name="TerminiTerapije[' + i + '].VremeDatumTerapije" type="text" value="' + termin.vremeDatumTerapije + '"></td>' +
                            '<td class="statusTermina" hidden> </td>' +
                            '<td class="btn move-row">Izaberi</td>' +
                            '</tr> ';
                    });
                    $('#table-termini-body').html(termini);
                }
            }
        });
    }
});

function filterAnalize() {
    var tekstPretrage = $('#analizePretraga').val().toUpperCase();

    $('#analizeTableBody tr').each(function () {
        var nazivAnalize = $(this).find('td.nazivAnalize').text().toUpperCase();
        nazivAnalize.includes(tekstPretrage) ? $(this).show() : $(this).hide();
    });
}

$(document).ready(function () {
    $('#karticePretraga').keypress(function (e) {

        var keycode = (e.keyCode ? e.keyCode : e.which);
        if (keycode === 13) {
            filterKarticaZaEvidenciju();
        }
    });

    $('#analizePretraga').keypress(function (e) {

        var keycode = (e.keyCode ? e.keyCode : e.which);
        if (keycode === 13) {
            filterAnalize();
        }
    });
});

function filterKarticaZaEvidenciju() {
    var tekstPretrage = $('#karticePretraga').val().toUpperCase();

    $('#karticeTableBody tr').each(function () {
        var nazivPazijenta = $(this).find('td.nazivPacijenta').text().toUpperCase();
        nazivPazijenta.includes(tekstPretrage) ? $(this).show() : $(this).hide();
    });
}

//$(document).ready(function () {
//    $('#karticePretraga').keypress(function (e) {

//        var keycode = (e.keyCode ? e.keyCode : e.which);
//        if (keycode === 13) {
//            filterKarticaZaEvidenciju();
//        }
//    });
//});

$(document).ajaxStart(function () {
    $("#loadingModal").modal({
        backdrop: 'static',
        keyboard: false
    });
}).ajaxStop(function () {
    $("#loadingModal").modal('hide');
});
