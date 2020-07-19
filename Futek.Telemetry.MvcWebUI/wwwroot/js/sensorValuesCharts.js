"use strict";


var connectButton = document.getElementById("connectButton");
var disconnectButton = document.getElementById("disconnectButton");
var testTheCommunicationButton = document.getElementById("testTheCommunicationButton");
var startReceivingDataButton = document.getElementById("startReceivingDataButton");
var stopReceivingDataButton = document.getElementById("stopReceivingDataButton");

connectButton.addEventListener("click", connect)
disconnectButton.addEventListener("click", disconnect)
testTheCommunicationButton.addEventListener("click", testTheCommunication)
startReceivingDataButton.addEventListener("click", startReceivingData)
stopReceivingDataButton.addEventListener("click", stopReceivingData)

disconnectButton.disabled = true;
startReceivingDataButton.disabled = true;
stopReceivingDataButton.disabled = true;
testTheCommunicationButton.disabled = true;

function connect() {
    $.get("/connection/connect").then(function (data, status) {

        if (status == "success") {
            connectButton.disabled = true;
            disconnectButton.disabled = false;
            startReceivingDataButton.disabled = false;
            testTheCommunicationButton.disabled = false;
            showAlert("Bağlantı gerçekleştirildi.");
        }
    },
        function (data, status) {

            disconnectButton.disabled = true;
            startReceivingDataButton.disabled = true;
            stopReceivingDataButton.disabled = true;
            testTheCommunicationButton.disabled = true;
            showAlert("Bağlantı Kurulurken Bir Hata Oluştu!!!.");
        }

    );

}
function disconnect() {
    showAlert("Bağlantı Kapatılıyor...")
    $.get("/connection/disconnect")
        .then(
            function (data, status) {
                if (status == "success") {
                    disconnectButton.disabled = true;
                    startReceivingDataButton.disabled = true;
                    stopReceivingDataButton.disabled = true;
                    testTheCommunicationButton.disabled = true;
                    connectButton.disabled = !(disconnectButton.disabled)
                    showAlert("Bağlantı Kapatıldı.")
                } else {
                    disconnectButton.disabled = false;
                    showAlert("Bağlantı Kapatılırken Bir Hata Oluştu!")
                }
            }
        )
}
function testTheCommunication() {
    $.get("/connection/testTheCommunication").then(function (data, status) {
        if (status == "success") {
            showAlert("Test İletişimi Başarılı.")
        }
    },
        function (data, status) {
            showAlert("Test İletişimi Başarısız!!!")
        }
    )

}
function startReceivingData() {
    $.get("/connection/startReceivingData").then(function (data, status) {
        if (status == "success") {
            startReceivingDataButton.disabled = true;
            stopReceivingDataButton.disabled = false;
            showAlert("Veriler Alınıyor.");
        }
    },
        function (data, status) {
            startReceivingDataButton.disabled = false;
            showAlert("Veriler Alınırken Bir Hata Oluştu!")
        });
}
function stopReceivingData() {
    showAlert("Veri Alma İşlemi Durduruluyor...")
    $.get("/connection/stopReceivingData").then(function (data, status) {
        if (status == "success") {
            stopReceivingDataButton.disabled = true;
            showAlert("Veri Alma İşlemi Durduruldu.")
            startReceivingDataButton.disabled = false;
        }
    },
        function (data, status) {
            stopReceivingDataButton.disabled = false;
            startReceivingDataButton.disabled = true;
            showAlert("Veri Alma İşlemi Durdurulurken Bir Hata Oluştu!")
        }
    );
}
function showAlert(message) {
    document.getElementById("alertMessage").textContent = message;
}


/*---------------- Charts ----------------*/

var speedGauge = Gauge(document.getElementById("speedGauge"), {
    max: 200,
    dialStartAngle: 45,
    dialEndAngle: 135,
    value: 105,
    label: function (value) {
        return Math.round(value) + " Km/h";
    }
});

var tempratureGauge = new LinearGauge({
    renderTo: 'tepmratureGauge',
    type: "linear-gauge",
    width: 120,
    height: 350,
    minValue: 0,
    maxValue: 120,
    borders: false,
    borderRadius: 25,
    barStrokeWidth: 8,
    exactTicks: true,
    valueInt: 3,
    valueDec: 0,
    minorTicks: 5,
    majorTicks: [0, 20, 40, 60, 80, 100, 120],
    barWitdth: 1,
    animationRule: "cycle",
    animationDuration: 500,
    needleWidth: 7,
    title: "Motor Sıcaklığı",
    units: "°C",
    animatedValue: true,
    colorPlate: "#0000",
    colorPlateEnd: "#4fff",
    colorTitle: "#fff",
    colorUnits: "#fff",
    colorBarProgress: "#f00",
    colorNeedle: "#4ff",
    colorNeedleEnd: "",
    colorNumbers: "#4ff",
    colorValueText: "#4ff",
    colorNeedleEnd: "#fff",
    fontValueSize: 35,
    valueBoxStroke: 4,
    fontTitleSize: 40,
    fontUnitsSize: 20,

    highlights: [{
        from: 0,
        to: 10,
        color: "#00f"
    },
    {
        from: 10,
        to: 20,
        color: "#30f"
    },
    {
        from: 20,
        to: 30,
        color: "#60f"
    },
    {
        from: 30,
        to: 40,
        color: "#90f"
    }, {
        from: 40,
        to: 50,
        color: "#c0f"
    },
    {
        from: 50,
        to: 60,
        color: "#f0f"
    },
    {
        from: 60,
        to: 70,
        color: "#f0e"
    },
    {
        from: 70,
        to: 80,
        color: "#f0b"
    },
    {
        from: 80,
        to: 90,
        color: "#f08"
    }, {
        from: 90,
        to: 100,
        color: "#f05"
    }, {
        from: 100,
        to: 110,
        color: "#f02"
    }, {
        from: 110,
        to: 120,
        color: "#f00"
    }],
    colorValueBoxBackground: "#fff0",
    colorValueBoxRect: "#fffb",
    colorValueBoxRectEnd: "#fff2",
    colorValueBoxShadow: "#fff1",
    colorValueText: "#fff"
}).draw();
tempratureGauge.value = 45
var cellChart;
am4core.ready(function () {

    // Themes begin
    am4core.useTheme(am4themes_animated);
    // Themes end

    // Create chart instance
    cellChart = am4core.create("cellDiv", am4charts.XYChart3D);

    var title = cellChart.titles.create()
    title.text = "Pil Hücre Durumları";
    title.fontSize = 30;
    title.marginBottom = 20;
    title.fill = am4core.color("#fff")


    // Add data
    cellChart.data = [
        {
            "category": "1",
            "value1": 90,
            "value2": 10
        },
        {
            "category": "2",
            "value1": 90,
            "value2": 10
        },
        {
            "category": "3",
            "value1": 85,
            "value2": 15
        },
        {
            "category": "4",
            "value1": 90,
            "value2": 10
        },
        {
            "category": "5",
            "value1": 75,
            "value2": 25
        },
        {
            "category": "6",
            "value1": 75,
            "value2": 25
        },
        {
            "category": "7",
            "value1": 95,
            "value2": 5
        },
        {
            "category": "8",
            "value1": 95,
            "value2": 5
        },
        {
            "category": "9",
            "value1": 95,
            "value2": 5
        },
        {
            "category": "10",
            "value1": 90,
            "value2": 10
        },
        {
            "category": "11",
            "value1": 90,
            "value2": 10
        },
        {
            "category": "12",
            "value1": 85,
            "value2": 15
        },
    ];

    // Create axes
    var categoryAxis = cellChart.xAxes.push(new am4charts.CategoryAxis());
    categoryAxis.dataFields.category = "category";
    categoryAxis.renderer.grid.template.location = 0;
    categoryAxis.renderer.grid.template.strokeOpacity = 0;
    categoryAxis.renderer.labels.template.fill = am4core.color("#aaa")
    categoryAxis.renderer.minGridDistance = 20;


    var valueAxis = cellChart.yAxes.push(new am4charts.ValueAxis());
    valueAxis.renderer.grid.template.strokeOpacity = 0;
    valueAxis.min = 0;
    valueAxis.max = 110;
    valueAxis.strictMinMax = true;
    valueAxis.renderer.baseGrid.disabled = true;
    valueAxis.renderer.labels.template.fill = am4core.color("#4ff")
    valueAxis.renderer.labels.template.adapter.add("text", function (text) {
        if ((text > 100) || (text < 0)) {
            return "";
        }
        else {
            return text + "%";
        }
    })


    // Create series
    var series1 = cellChart.series.push(new am4charts.ConeSeries());
    series1.dataFields.valueY = "value1";
    series1.dataFields.categoryX = "category";
    series1.columns.template.width = am4core.percent(80);
    series1.columns.template.fill = am4core.color("#0f0")
    series1.columns.template.fillOpacity = 1;
    series1.columns.template.strokeOpacity = 1;
    series1.columns.template.strokeWidth = 2;

    var series2 = cellChart.series.push(new am4charts.ConeSeries());
    series2.dataFields.valueY = "value2";
    series2.dataFields.categoryX = "category";
    series2.stacked = true;
    series2.columns.template.width = am4core.percent(80);
    series2.columns.template.fill = am4core.color("#4ff");
    series2.columns.template.fillOpacity = 0.15;
    series2.columns.template.stroke = am4core.color("#4ff");
    series2.columns.template.strokeOpacity = 0.3;
    series2.columns.template.strokeWidth = 3;

});

var energyChartConfig = liquidFillGaugeDefaultSettings();
energyChartConfig.circleColor = "#ff0";
energyChartConfig.textColor = "#888";
energyChartConfig.waveTextColor = "#fff";
energyChartConfig.waveColor = "#5bf";
energyChartConfig.circleThickness = 0.05;
energyChartConfig.textVertPosition = 0.5;
energyChartConfig.waveAnimateTime = 1300;
var energyChart = loadLiquidFillGauge("energyChart", 85, energyChartConfig);


/*--------------- SignalR Connection -----------------------*/

var connection = new signalR.HubConnectionBuilder().withUrl("/sensorValuesHub").build();

connection.start().then(function () {
    console.log("iletisim basladı")
}).catch(function (err) {
    return console.error(err.toString());
});

var cellDatas = [12];
var cell = {};
connection.on("receiveValues", function (sensorValuesDto) {
    console.log(sensorValuesDto)

    sensorValuesDto.sensorValues.forEach(function (sensorValue) {
        
        if (sensorValue.sensorId == 1) {
            speedGauge.setValueAnimated(sensorValue.value)
        } else if (sensorValue.sensorId == 2) {
            tempratureGauge.value = sensorValue.value
        } else if (sensorValue.sensorId == 3) {
            energyChart.update(sensorValue.value);
        }
        else {
            if (sensorValue.sensorId == 4) {
                cell = {
                    "category": "1",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": (100 - sensorValue.value)
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 5) {
                cell = {
                    "category": "2",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 6) {
                cell = {
                    "category": "3",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 7) {
                cell = {
                    "category": "4",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 8) {
                cell = {
                    "category": "5",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 9) {
                cell = {
                    "category": "6",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 10) {
                cell = {
                    "category": "7",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 11) {
                cell = {
                    "category": "8",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 12) {
                cell = {
                    "category": "9",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 13) {
                cell = {
                    "category": "10",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 14) {
                cell = {
                    "category": "11",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            } else if (sensorValue.sensorId == 15) {
                cell = {
                    "category": "12",//TODO ajax get ile sensor adı alınacak
                    "value1": sensorValue.value,
                    "value2": 100 - sensorValue.value
                }
                cellDatas[(sensorValue.sensorId - 4)] = cell;
            }

        } 

    })

    cellChart.data = cellDatas;
});

