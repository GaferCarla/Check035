



function downloadPdf() {
    $("#downloadPDF").hide;
    window.print();
    //$("#downloadPDF").show;
}

function Report_Gender() {
    //var id_sexo = $('#ddw_Sexo option:selected').val();
    //var id_age = $('#ddw_Edades option:selected').val();
    //var id_edo_civil = $('#ddw_Edo_Civi option:selected').val();
    var id_empresa = $('#id_empresa').val();
    var id_survey = $('#id_survey').val(); 

    $.ajax({
        type: 'POST',
        url: '@Url.Action("Searching_Gender")',
        dataType: 'json',
        data: { id_empresa: id_empresa, id_survey: id_survey },
        success: function (dato) {
            if (dato != null) {

                //#############################################################################################################################################################################################################################
                //google.charts.load("current", { packages: ["corechart"] });
                //google.charts.setOnLoadCallback(drawChart);
                //function drawChart() {
                //    var data = google.visualization.arrayToDataTable([
                //        ['Task', 'Hours per Day'],
                //        ['Hombres', dato.Total_Surveys_H],
                //        ['Mujeres', dato.Total_Surveys_M]
                //    ]);

                //    var options = {
                //        title: 'Estadísticas por Genero',
                //        pieHole: 0.4,
                //        slices: { 0: { color: "RED" }, 1: { color: "Yellow" } }

                //    };

                //    var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
                //    chart.draw(data, options);
                //}

                //#############################################################################################################################################################################################################################
                //var chart = c3.generate({
                //    bindto: '#chart',
                //    data: {
                //        columns: [
                //            ['data1', 30, 200, 100, 400, 150, 250],
                //            ['data2', 50, 20, 10, 40, 15, 25]
                //        ],
                //        axes: {
                //            data2: 'y2'
                //        }
                //    },
                //    axis: {
                //        y: {
                //            label: { // ADD
                //                text: 'Y Label',
                //                position: 'outer-middle'
                //            }
                //        },
                //        y2: {
                //            show: true,
                //            label: { // ADD
                //                text: 'Y2 Label',
                //                position: 'outer-middle'
                //            }
                //        }
                //    }
                //});

                //var chart = c3.generate({
                //    data: {
                //        // iris data from R
                //        columns: [
                //            ['data1', 30],
                //            ['data2', 120],
                //        ],
                //        type: 'pie',
                //        onclick: function (d, i) { console.log("onclick", d, i); },
                //        onmouseover: function (d, i) { console.log("onmouseover", d, i); },
                //        onmouseout: function (d, i) { console.log("onmouseout", d, i); }
                //    }
                //});

                //setTimeout(function () {
                //    chart.load({
                //        columns: [
                //            ["setosa", 0.2, 0.2, 0.2, 0.2, 0.2, 0.4, 0.3, 0.2, 0.2, 0.1, 0.2, 0.2, 0.1, 0.1, 0.2, 0.4, 0.4, 0.3, 0.3, 0.3, 0.2, 0.4, 0.2, 0.5, 0.2, 0.2, 0.4, 0.2, 0.2, 0.2, 0.2, 0.4, 0.1, 0.2, 0.2, 0.2, 0.2, 0.1, 0.2, 0.2, 0.3, 0.3, 0.2, 0.6, 0.4, 0.3, 0.2, 0.2, 0.2, 0.2],
                //            ["versicolor", 1.4, 1.5, 1.5, 1.3, 1.5, 1.3, 1.6, 1.0, 1.3, 1.4, 1.0, 1.5, 1.0, 1.4, 1.3, 1.4, 1.5, 1.0, 1.5, 1.1, 1.8, 1.3, 1.5, 1.2, 1.3, 1.4, 1.4, 1.7, 1.5, 1.0, 1.1, 1.0, 1.2, 1.6, 1.5, 1.6, 1.5, 1.3, 1.3, 1.3, 1.2, 1.4, 1.2, 1.0, 1.3, 1.2, 1.3, 1.3, 1.1, 1.3],
                //            ["virginica", 2.5, 1.9, 2.1, 1.8, 2.2, 2.1, 1.7, 1.8, 1.8, 2.5, 2.0, 1.9, 2.1, 2.0, 2.4, 2.3, 1.8, 2.2, 2.3, 1.5, 2.3, 2.0, 2.0, 1.8, 2.1, 1.8, 1.8, 1.8, 2.1, 1.6, 1.9, 2.0, 2.2, 1.5, 1.4, 2.3, 2.4, 1.8, 1.8, 2.1, 2.4, 2.3, 1.9, 2.3, 2.5, 2.3, 1.9, 2.0, 2.3, 1.8],
                //        ]
                //    });
                //}, 1500);

                //setTimeout(function () {
                //    chart.unload({
                //        ids: 'data1'
                //    });
                //    chart.unload({
                //        ids: 'data2'
                //    });
                //}, 2500);

                document.getElementById("chartContainer").style.display = "block";

                //####################################################CANVASJS#########################################################################################################################################################################
                var chart = new CanvasJS.Chart("chartContainer", {
                    animationEnabled: true,
                    title: {
                        text: "Estadísticas por Genero",
                        horizontalAlign: "center",
                        fontSize: 20,
                        fontFamily: "Arial"
                    }, legend: {
                        horizontalAlign: "right",
                        verticalAlign: "center"
                    },
                    data: [{
                        type: "doughnut",
                        startAngle: 60,
                        innerRadius: 60,
                        showInLegend: true,
                        indexLabelFontSize: 14,
                        indexLabel: "{name} - #percent%",
                        toolTipContent: "<b>{name}</b><br> <b> {label}  <br>Encuestas:  {y} <br> Porcentaje: #percent% </b>",
                        dataPoints: [
                            { y: dato.Total_Surveys_H, name: "Hombres", label: "Nivel: " + dato.Final_Beredict_H, color: dato.Color_Nivel_H },
                            { y: dato.Total_Surveys_M, name: "Mujeres", label: "Nivel: " + dato.Final_Beredict_M, color: dato.Color_Nivel_M },
                        ]
                    }]
                });

                chart.render();

                $("a.canvasjs-chart-credit").html('');
                $("canvas.canvasjs-chart-credit").html('');

                //#############################################################################################################################################################################################################################

                //am4core.ready(function () {

                //    // Themes begin
                //    am4core.useTheme(am4themes_animated);
                //    // Themes end

                //    var chart = am4core.create("chartdiv", am4charts.PieChart3D);
                //    chart.hiddenState.properties.opacity = 1; // this creates initial fade-in

                //    chart.data = [
                //        {
                //            Persona: "Hombres",
                //            Genero: dato.Total_Surveys_H,
                //            Color: "red"
                //        },
                //        {
                //            Persona: "Mujeres",
                //            Genero: dato.Total_Surveys_M,
                //            Color: "red"
                //        }
                //    ];

                //    chart.innerRadius = am4core.percent(50);

                //    chart.depth = 25;

                //    chart.legend = new am4charts.Legend();

                //    var series = chart.series.push(new am4charts.PieSeries3D());
                //    series.dataFields.value = "Genero";
                //    series.dataFields.depthValue = "Genero";
                //    series.dataFields.category = "Persona";
                //    series.colors.list = [
                //        am4core.color(dato.Color_Nivel_H),
                //        am4core.color(dato.Color_Nivel_M),
                //    ];

                //});


                //#############################################################################################################################################################################################################################
                //alert(dato.Final_AVG);
                //alert(dato.Final_AVG_H);
                //alert(dato.Final_AVG_M);
                //    var ctx = document.getElementById("Grafico_Sexo");
                //      var myChart = new Chart(ctx, {
                //          type: 'doughnut',
                //          data: {
                //              labels: ["Hombres", "Mujeres"],
                //              datasets: [{
                //                  backgroundColor: [dato.Color_Nivel_H, dato.Color_Nivel_M],
                //                  data: [dato.Total_Surveys_H, dato.Total_Surveys_M],
                //                  borderWidth: 1
                //              }]
                //          },
                //          options: {
                //              title: {
                //                  display: true,
                //                  text: 'Estadísticas por Sexo'
                //              }
                //          }
                //      });


            }
            //@*var Final_AVG_H = (int)@ViewBag.Final_AVG_H;
            //var Final_AVG_M = (int)@ViewBag.Final_AVG_M.Categoria_5_G;
            //var total = dato[0].Total_AVG_Cat_1;
            //alert(total, Final_AVG_H, Final_AVG_M);*@



            },
        error: function () {
            alert("No Existen Suficientes Registros Para Generar Estadísticas");
        },
        catch(e) {
            alert(e.name + "\n" + e.message)
        }
    });
}

function Report_Nivel_Estudios() {
    var id_empresa = $('#id_empresa').val();
    var id_survey = @ViewBag.Survey;

    document.getElementById("chartNivel_Estudios").style.display = "block";
    $.ajax({
        type: 'POST',
        url: '@Url.Action("Searching_Nivel_Estudios")',
        dataType: 'json',
        data: { id_empresa: id_empresa, id_survey: id_survey },
        success: function (dato) {
            if (dato != null) {
                var chart = new CanvasJS.Chart("chartNivel_Estudios", {
                    animationEnabled: true,
                    title: {
                        text: "Estadísticas por Nivel de Estudios",
                        horizontalAlign: "center",
                        fontSize: 20,
                        fontFamily: "Arial"
                    }, legend: {
                        horizontalAlign: "right",
                        verticalAlign: "center"
                    },
                    data: [{
                        type: "doughnut",
                        startAngle: 60,
                        innerRadius: 60,
                        showInLegend: true,
                        indexLabelFontSize: 14,
                        indexLabel: "{name} - #percent%",
                        toolTipContent: "<b>{name}</b><br> <b> {label}  <br>Encuestas:  {y} <br> Porcentaje: #percent% </b>",
                        dataPoints: [
                            { y: dato.Total_Surveys_1, name: "Sin Información", label: "Nivel: " + dato.Final_Beredict_1, color: dato.Color_Nivel_1 },
                            { y: dato.Total_Surveys_2, name: "Primaria", label: "Nivel: " + dato.Final_Beredict_2, color: dato.Color_Nivel_2 },
                            { y: dato.Total_Surveys_3, name: "Secundaria", label: "Nivel: " + dato.Final_Beredict_3, color: dato.Color_Nivel_3 },
                            { y: dato.Total_Surveys_4, name: "Bachillerato o Preparatoria", label: "Nivel: " + dato.Final_Beredict_4, color: dato.Color_Nivel_4 },
                            { y: dato.Total_Surveys_5, name: "Tecnico Superior", label: "Nivel: " + dato.Final_Beredict_5, color: dato.Color_Nivel_5 },
                            { y: dato.Total_Surveys_6, name: "Licenciatura", label: "Nivel: " + dato.Final_Beredict_6, color: dato.Color_Nivel_6 },
                            { y: dato.Total_Surveys_7, name: "Maestría", label: "Nivel: " + dato.Final_Beredict_7, color: dato.Color_Nivel_7 },
                            { y: dato.Total_Surveys_8, name: "Doctorado", label: "Nivel: " + dato.Final_Beredict_8, color: dato.Color_Nivel_8 },
                        ]
                    }]
                });

                chart.render();

                $("a.canvasjs-chart-credit").html('');
                $("canvas.canvasjs-chart-credit").html('');

            }
        },
        error: function () {
            alert("No Existen Suficientes Registros Para Generar Estadísticas");
        },
        catch(e) {
            alert(e.name + "\n" + e.message)
        }
    });
}


function Report_Edo_Civil() {
    //var id_sexo = $('#ddw_Sexo option:selected').val();
    //var id_age = $('#ddw_Edades option:selected').val();
    //var id_edo_civil = $('#ddw_Edo_Civi option:selected').val();
    var id_empresa = $('#id_empresa').val();
    var id_survey = @ViewBag.Survey;

    document.getElementById("chartEdo_Civil").style.display = "block";
    $.ajax({
        type: 'POST',
        url: '@Url.Action("Searching_Edo_Civil")',
        dataType: 'json',
        data: { id_empresa: id_empresa, id_survey: id_survey },
        success: function (dato) {
            if (dato != null) {
                var chart = new CanvasJS.Chart("chartEdo_Civil", {
                    animationEnabled: true,
                    title: {
                        text: "Estadísticas por Estado Civil",
                        horizontalAlign: "center",
                        fontSize: 20,
                        fontFamily: "Arial"
                    }, legend: {
                        horizontalAlign: "right",
                        verticalAlign: "center"
                    },
                    data: [{
                        type: "doughnut",
                        startAngle: 60,
                        innerRadius: 60,
                        showInLegend: true,
                        indexLabelFontSize: 14,
                        indexLabel: "{name} - #percent%",
                        toolTipContent: "<b>{name}</b><br> <b> {label}  <br>Encuestas:  {y} <br> Porcentaje: #percent% </b>",
                        dataPoints: [
                            { y: dato.Total_Surveys_CA, name: "Casados", label: "Nivel: " + dato.Final_Beredict_CA, color: dato.Color_Nivel_CA },
                            { y: dato.Total_Surveys_SO, name: "Solteros", label: "Nivel: " + dato.Final_Beredict_SO, color: dato.Color_Nivel_SO },
                            { y: dato.Total_Surveys_DI, name: "Divorciados", label: "Nivel: " + dato.Final_Beredict_DI, color: dato.Color_Nivel_DI },
                            { y: dato.Total_Surveys_UL, name: "Unión Libre", label: "Nivel: " + dato.Final_Beredict_UL, color: dato.Color_Nivel_UL },
                            { y: dato.Total_Surveys_VI, name: "Viudos", label: "Nivel: " + dato.Final_Beredict_VI, color: dato.Color_Nivel_VI },
                        ]
                    }]
                });

                chart.render();

                $("a.canvasjs-chart-credit").html('');
                $("canvas.canvasjs-chart-credit").html('');

            }
        },
        error: function () {
            alert("No Existen Suficientes Registros Para Generar Estadísticas");
        },
        catch(e) {
            alert(e.name + "\n" + e.message)
        }
    });
}

function Report_Edades() {
    var id_empresa = $('#id_empresa').val();
    var id_survey = @ViewBag.Survey;

    document.getElementById("chartEdades").style.display = "block";
    $.ajax({
        type: 'POST',
        url: '@Url.Action("Searching_Edades")',
        dataType: 'json',
        data: { id_empresa: id_empresa, id_survey: id_survey },
        success: function (dato) {
            if (dato != null) {

                var ctx = document.getElementById("chartEdades");
                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: ["15 - 19 años", "20 - 24 años", "25 - 29 años", "30 - 34 años", "35 - 39 años", "40 - 44 años", "45 - 49 años", "50 - 54 años", "55 - 59 años", "60 - 64 años", "65 - 69 años", "70 o más años"],
                        datasets: [{
                            backgroundColor: [dato.Color_Nivel_1, dato.Color_Nivel_2, dato.Color_Nivel_3, dato.Color_Nivel_4, dato.Color_Nivel_5, dato.Color_Nivel_6, dato.Color_Nivel_7, dato.Color_Nivel_8, dato.Color_Nivel_9, dato.Color_Nivel_10, dato.Color_Nivel_11, dato.Color_Nivel_12],
                            data: [dato.Total_Surveys_1, dato.Total_Surveys_2, dato.Total_Surveys_3, dato.Total_Surveys_4, dato.Total_Surveys_5, dato.Total_Surveys_6, dato.Total_Surveys_7, dato.Total_Surveys_8, dato.Total_Surveys_9, dato.Total_Surveys_10, dato.Total_Surveys_11, dato.Total_Surveys_12],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        legend: {
                            labels: ["Edad", "Edad"],
                            display: false,
                            labels: {
                                fontColor: 'rgb(255, 99, 132)'
                            }
                        },
                        title: {
                            display: true,
                            text: 'Estadísticas por Edad',
                            fontSize: 20
                        },
                        scales: {
                            yAxes: [{
                                ticks: {
                                    // Include a dollar sign in the ticks
                                    //callback: function (value, index, values) {
                                    //    return 'Encuestas ' + value;
                                    //},
                                    beginAtZero: true,
                                    userCallback: function (label, index, labels) {
                                        // when the floored value is the same as the value we have a whole number
                                        if (Math.floor(label) === label) {
                                            return 'Encuestas ' + label;
                                        }

                                    },
                                }
                            }]
                        }

                    }
                });


            } else {
                alert("Información Faltante");
            }

        },
        error: function () {
            alert("No Existen Suficientes Registros Para Generar Estadísticas");
        },
        catch(e) {
            alert(e.name + "\n" + e.message)
        }
    });
}