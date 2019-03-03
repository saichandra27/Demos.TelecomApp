var fieldvalue = ""; var fieldname = ""; var refid = "";

$(document).ajaxStart(function () {
    $("#loading").show();
    $(".log").html("<h1>Loading.....................</h1>");
});

$(document).ajaxStop(function () {
    $("#loading").hide();
    $(".log").html("Triggered ajaxStop handler.");
});

window.onload = getdata;

function getdata() {
 
    $.ajax({
        type: "POST",
        url: "../TowersService.asmx/getData",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            gridDisplay(result.d);
        },
        error: function (e) {
            alert(e.status);
        }
    });
    var oPushButton1 = new YAHOO.widget.Button("btnfind");
    oPushButton1.on("click", getdatabySiteId);
    
    var btnpop = new YAHOO.widget.Button("btnpop");
    btnpop.on("click", getdatabypop);

    var btnengname = new YAHOO.widget.Button("btnengname");
    btnengname.on("click",  getdatabyengname);

    var btndistrict = new YAHOO.widget.Button("btndistrict");
    btndistrict.on("click", getdatabydistrict);
}

function getdatabySiteId() {
    var siteid = $("#siteidinput").val();
    $.ajax({
        type: "POST",
        url: "../TowersService.asmx/getDatabySiteId",
        data: '{"SiteId": "' + siteid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            gridDisplay(result.d);
        },
        error: function (e) {
            alert(e.status);
        }
    });
}

function getdatabyengname() {
    var siteid = $("#engnameinput").val();
    $.ajax({
        type: "POST",
        url: "../TowersService.asmx/getDatabyengname",
        data: '{"engname": "' + siteid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            gridDisplay(result.d);
        },
        error: function (e) {
            alert(e.status);
        }
    });
}

function getdatabypop() {
    var siteid = $("#popinput").val();
    $.ajax({
        type: "POST",
        url: "../TowersService.asmx/getDatabyPOP",
        data: '{"POP": "' + siteid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            gridDisplay(result.d);
        },
        error: function (e) {
            alert(e.status);
        }
    });
}

function getdatabydistrict() {
    var siteid = $("#districtinput").val();
    $.ajax({
        type: "POST",
        url: "../TowersService.asmx/getDatabydistrict",
        data: '{"district": "' + siteid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            gridDisplay(result.d);
        },
        error: function (e) {
            alert(e.status);
        }
    });
}

function gridDisplay(result) {

    // Define Columns

    var gridColumns = [
                        { key: "id", sortable: true, resizeable: true, formatter: YAHOO.widget.DataTable.formatLink },
                        { key: "SiteId", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "District", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "EngineerName", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "POP", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "Cell", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "Ckt", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "EqptatAEnd", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "Port_A", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "EqptatbEnd", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "Port_B", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "Connectivity", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "RBL2", sortable: true, resizeable: true, editor: new YAHOO.widget.TextboxCellEditor() },
                        { key: "Delete", label: "Delete", formatter: YAHOO.widget.DataTable.formatButton }
    ];

    var gridsource = new YAHOO.util.DataSource(result);

    gridsource.responseSchema = {
        fields: ["id", "SiteId", "District", "Cell", "Ckt", "EqptatAEnd", "Port_A", "EqptatbEnd", "Port_B", "POP", "Connectivity", "EngineerName", "RBL2"]
    };

    var oConfigs = {
        paginator: new YAHOO.widget.Paginator({
            rowsPerPage: 15
        }),
        initialRequest: "results=504"
    };

    var myDataTable = new YAHOO.widget.DataTable("columnshowhide", gridColumns, gridsource, oConfigs, { caption: "DataTable Caption" });




    // Create a Custom Event handler

    var selectedcell = function (oArgs) {
        myDataTable.onEventShowCellEditor(oArgs);
        var target = oArgs.target;
        record = this.getRecord(target);
        column = this.getColumn(target);
        fieldname = column.key;
        fieldvalue = record.getData(column.key);
        refid = record.getData("id");
        if (column.key == "id") {
            $.ajax({
                type: "POST",
                url: "../TowersService.asmx/getDatabyId",
                data: '{"id": "' + refid + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    buidlEditform(result.d);
                },
                error: function (e) {
                    alert(e.status);
                }
            });
        }
    }


    var myHandler = function (oArgs) {
        var oEditor = oArgs.editor;
        var newData = oArgs.newData;
        var oldData = oArgs.oldData;
        // var value = "filedname:" + fieldname + "filedvalue:" + fieldvalue + "refid:" + refid;
        $.ajax({
            type: "POST",
            url: "../TowersService.asmx/updateData",
            data: '{"idvalue": "' + refid + '","fieldname": "' + fieldname + '","fieldvalue": "' + newData + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {

            },
            error: function (e) {
                alert(e.status);
            }
        });
    }

    var deleterecord = function (oArgs) {
        var target = oArgs.target;
        record = this.getRecord(target);
        refid = record.getData("id");
        var cl = this.getColumn(target);
        if (cl.key == "Delete") {
            if (confirm("Are you sure you want to delete" + record.getData("id"))) {
                myDataTable.deleteRow(target);
                $.ajax({
                    type: "POST",
                    url: "../TowersService.asmx/deleteData",
                    data: '{"idvalue": "' + refid + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                    },
                    error: function (e) {
                        alert(e.status);
                    }
                });
            }
        }
        else {
            alert('you pressed link button');
        }

    }

    myDataTable.subscribe("rowMouseoverEvent", myDataTable.onEventHighlightRow);
    myDataTable.subscribe("rowMouseoutEvent", myDataTable.onEventUnhighlightRow);
    myDataTable.subscribe("cellClickEvent", selectedcell);
    myDataTable.subscribe("editorSaveEvent", myHandler);
    myDataTable.subscribe("buttonClickEvent", deleterecord);
    myDataTable.subscribe("linkClickEvent", deleterecord);


    var newCols = true;

    var showDlg = function (e) {

        $('.hd').show();

        YAHOO.util.Event.stopEvent(e);

        if (newCols) {
            // Populate Dialog
            // Using a template to create elements for the SimpleDialog
            var allColumns = myDataTable.getColumnSet().keys;
            var elPicker = YAHOO.util.Dom.get("dt-dlg-picker");
            var elTemplateCol = document.createElement("div");
            YAHOO.util.Dom.addClass(elTemplateCol, "dt-dlg-pickercol");
            var elTemplateKey = elTemplateCol.appendChild(document.createElement("span"));
            YAHOO.util.Dom.addClass(elTemplateKey, "dt-dlg-pickerkey");
            var elTemplateBtns = elTemplateCol.appendChild(document.createElement("span"));
            YAHOO.util.Dom.addClass(elTemplateBtns, "dt-dlg-pickerbtns");
            var onclickObj = { fn: handleButtonClick, obj: this, scope: false };

            // Create one section in the SimpleDialog for each Column
            var elColumn, elKey, oButtonGrp;
            for (var i = 0, l = allColumns.length; i < l; i++) {
                var oColumn = allColumns[i];

                // Use the template
                elColumn = elTemplateCol.cloneNode(true);

                // Write the Column key
                elKey = elColumn.firstChild;
                elKey.innerHTML = oColumn.getKey();

                // Create a ButtonGroup
                oButtonGrp = new YAHOO.widget.ButtonGroup({
                    id: "buttongrp" + i,
                    name: oColumn.getKey(),
                    container: elKey.nextSibling
                });
                oButtonGrp.addButtons([
                { label: "Show", value: "Show", checked: ((!oColumn.hidden)), onclick: onclickObj },
                { label: "Hide", value: "Hide", checked: ((oColumn.hidden)), onclick: onclickObj }
                ]);

                elPicker.appendChild(elColumn);
            }
            newCols = false;
        }
        myDlg.show();
    };

    var hideDlg = function (e) {
        this.hide();
    };

    var handleButtonClick = function (e, oSelf) {
        var sKey = this.get("name");
        if (this.get("value") === "Hide") {
            // Hides a Column
            myDataTable.hideColumn(sKey);
        }
        else {
            // Shows a Column
            myDataTable.showColumn(sKey);
        }
    };

    YAHOO.util.Dom.removeClass("dt-dlg", "inprogress");

    var myDlg = new YAHOO.widget.SimpleDialog("dt-dlg", {
        width: "30em", height: "30em",
        fixedcenter: true,
        visible: false,
        buttons: [{ text: "Close", handler: hideDlg }],
        constrainToViewport: true
    });


    myDlg.render();

    myDataTable.subscribe("columnReorderEvent", function () {
        newCols = true;
        YAHOO.util.Event.purgeElement("dt-dlg-picker", true);
        YAHOO.util.Dom.get("dt-dlg-picker").innerHTML = "";
    }, this, true);

    // Hook up the SimpleDialog to the link
    YAHOO.util.Event.addListener("dt-options-link", "click", showDlg, this, true);
    
}

function buidlEditform(result) {
    var txt =
       "<table><tr><th colspan='2' style ='background-color:#ECEDED' align='left'>Transmission Automation System</th><tr><td>Id:</td><td><div id='Id'>" + result[0].id + "</div></td></tr>" +
       "<tr><td>SiteId:</td><td><input id='SiteId' type='text' value =" + result[0].SiteId + " /></td></tr>" +
       "<tr><td>District:</td><td><input id='District' type='text' value =" + result[0].District + " /></td></tr>" +
       "<tr><td>Cell:</td><td><input id='Cell' type='text' value =" + result[0].Cell + " /></td></tr>" +
       "<tr><td>Ckt:</td><td><input id='Ckt' type='text' value =" + result[0].Ckt + " /></td></tr>" +
       "<tr><td>EqptatAEnd:</td><td><input id='EqptatAEnd' type='text' value =" + result[0].EqptatAEnd + " /></td></tr>" +
       "<tr><td>Port_A:</td><td><input id='Port_A' type='text' value =" + result[0].Port_A + " /></td></tr>" +
       "<tr><td>EqptatBEnd:</td><td><input id='EqptatBEnd' type='text' value =" + result[0].EqptatbEnd + " /></td></tr>" +
       "<tr><td>Port_B:</td><td><input id='Port_B' type='text' value =" + result[0].Port_B + " /></td></tr>" +
       "<tr><td>POP:</td><td><input id='POP' type='text' value =" + result[0].POP + " /></td></tr>" +
       "<tr><td>Connectivity:</td><td><input id='Connectivity' type='text' value =" + result[0].Connectivity + " /></td></tr>" +
       "<tr><td>EngineerName:</td><td><input id='EngineerName' type='text' value =" + result[0].EngineerName + " /></td></tr>" +
       "<tr><td>RBL2:</td><td><input id='RBL2' type='text' value =" + result[0].RBL2 + "></td></tr>" +
       "</table>";
    document.getElementById("Edit_From").innerHTML = txt;

    var saveDlg = function (e) {
        var updatedata = $("#Id").text() + ";" + $("#SiteId").val() + ";" + $("#District").val() + ";" + $("#Cell").val() + ";" +
                     $("#Ckt").val() + ";" + $("#EqptatAEnd").val() + ";" + $("#Port_A").val() + ";" + $("#EqptatBEnd").val() + ";" + $("#Port_B").val() + ";" +
                     $("#POP").val() + ";" + $("#Connectivity").val() + ";" + $("#EngineerName").val() + ";" + $("#RBL2").val();
        $.ajax({
            type: "POST",
            url: "../TowersService.asmx/updateDatabyId",
            data: '{"data": "' + updatedata + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
               
            },
            error: function (e) {
                alert(e.status);
            }
        });
        this.hide();
        $.ajax({
            type: "POST",
            url: "../TowersService.asmx/getData",
            data: {},
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                gridDisplay(result.d);
            },
            error: function (e) {
                alert(e.status);
            }
        });
    };

    var hideDlg = function (e) {
        this.hide();
    };

    var editdlg = new YAHOO.widget.Dialog("Edit_From", {
        width: "30em", height: "30em",
        fixedcenter: true,
        visible: false,
        buttons: [{ text: "Save", handler: saveDlg }, { text: "Cancel", handler: hideDlg }],
        constrainToViewport: true
    });

    editdlg.render();
    editdlg.show();
}


$.ajax({
    type: "POST",
    url: "../TowersService.asmx/getSiteId",
    data: {},
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
        buildsiteidautocomplete(result.d);
    },
    error: function (e) {
        alert(e.status);
    }
});

$.ajax({
    type: "POST",
    url: "../TowersService.asmx/getengineername",
    data: {},
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
        buildengnameautocomplete(result.d);
    },
    error: function (e) {
        alert(e.status);
    }
});


$.ajax({
    type: "POST",
    url: "../TowersService.asmx/getpop",
    data: {},
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
        buildpopautocomplete(result.d);
    },
    error: function (e) {
        alert(e.status);
    }
});

$.ajax({
    type: "POST",
    url: "../TowersService.asmx/getdistrict",
    data: {},
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
        builddistrictautocomplete(result.d);
    },
    error: function (e) {
        alert(e.status);
    }
});

function buildsiteidautocomplete(result) {
    var siteiddatasource = new YAHOO.util.DataSource(result);
    var siteidoDS = new YAHOO.util.LocalDataSource(result);
    // Instantiate the AutoComplete
    var siteidoAC = new YAHOO.widget.AutoComplete("siteidinput", "siteidcontainer", siteidoDS);
    siteidoAC.prehighlightClassName = "yui-ac-prehighlight";
    siteidoAC.useShadow = true;
}

function buildpopautocomplete(result) {
    var popdatasource = new YAHOO.util.DataSource(result);
    var popoDS = new YAHOO.util.LocalDataSource(result);
    // Instantiate the AutoComplete
    var popoAC = new YAHOO.widget.AutoComplete("popinput", "popcontainer", popoDS);
    popoAC.prehighlightClassName = "yui-ac-prehighlight";
    popoAC.useShadow = true;
}

function buildengnameautocomplete(result) {
    var engnamedatasource = new YAHOO.util.DataSource(result);
    var engnameoDS = new YAHOO.util.LocalDataSource(result);
    // Instantiate the AutoComplete
    var engnameoAC = new YAHOO.widget.AutoComplete("engnameinput", "engnamecontainer", engnameoDS);
    engnameoAC.prehighlightClassName = "yui-ac-prehighlight";
    engnameoAC.useShadow = true;
}

function builddistrictautocomplete(result) {
    var districtdatasource = new YAHOO.util.DataSource(result);
    var districtoDS = new YAHOO.util.LocalDataSource(result);
    // Instantiate the AutoComplete
    var districtoAC = new YAHOO.widget.AutoComplete("districtinput", "districtcontainer", districtoDS);
    districtoAC.prehighlightClassName = "yui-ac-prehighlight";
    districtoAC.useShadow = true;
}