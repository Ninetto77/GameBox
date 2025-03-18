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

//сохранить данные
SaveExtern: function(data){
   console.log("SaveExtern");
    var dataString = UTF8ToString(data);
    var myObj = JSON.parse(dataString);
    player.setData(myObj);
},

//загрузить данные
LoadExtern: function(){
   console.log("LoadExtern");
    player.getData().then(_data=>{
        const myJSON = JSON.stringify(_data);
        //имя объекта, функция, которая есть в этом объекте
        myGameInstance.SendMessage('Progress', 'SetPlayerInfo' , myJSON);
    });
},

////лидерборд
// InitLeaderboard: function ()
// {
//     InitLeaderboard();
// },

// SetLeaderboardScores: function (nameLB, score)
// {
//     var nameLBO = UTF8ToString(nameLB);
//     var scores = score;
//     SetLeaderboard (nameLBO, scores);
// },

/////GameplayAPI
SetStartGameplayAPI: function()
{
   // ysdk.features.GameplayAPI?.start(); 
},

SetStopGameplayAPI: function()
{
    //ysdk.features.GameplayAPI?.stop();
},


});