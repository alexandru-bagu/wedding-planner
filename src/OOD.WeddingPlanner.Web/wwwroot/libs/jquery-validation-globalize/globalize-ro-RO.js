
var paths = ['/libs/cldr-data/supplemental/likelySubtags.json',
  '/libs/cldr-data/main/ro/numbers.json',
  '/libs/cldr-data/supplemental/numberingSystems.json',
  '/libs/cldr-data/main/ro/ca-gregorian.json',
  '/libs/cldr-data/main/ro/timeZoneNames.json',
  '/libs/cldr-data/supplemental/timeData.json',
  '/libs/cldr-data/supplemental/weekData.json'];
$.when.apply(undefined, paths.map(function (path) {
    return $.getJSON(path);
})).then(function () {
  return [].slice.apply(arguments, [0]).map(function (result) {
    return result[0];
  });
}).then(Globalize.load).then(function(){
    Globalize.locale('ro');
});