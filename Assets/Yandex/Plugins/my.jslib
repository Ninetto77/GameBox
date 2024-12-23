mergeInto(LibraryManager.library, {

GetLang : function(){
var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
},

Hello: function(){
    window.alert("Hello, world");
},

SaveExtern: function(data){
    var dataString = UTF8ToString(data);
    var myObj = JSON.parse(dataString);
    player.setData(myObj);
},

LoadExtern: function(){
    player.getData().then(_data=>{
        const myJSON = JSON.stringify(_data);
        myGameInstance.SendMessage('Progress', 'LoadPlayerInfo' , myJSON);
    });
},


});