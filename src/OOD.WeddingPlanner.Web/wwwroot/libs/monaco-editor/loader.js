$(function(){
  var monacoLoader = document.createElement('script');
  monacoLoader.setAttribute('src','/libs/monaco-editor/min/vs/loader.js');
  document.body.appendChild(monacoLoader);

  var interval = setInterval(function(){
    if(window.require && window.require.config) {
      window.require.config({ paths: { vs: '/libs/monaco-editor/min/vs' } });
      clearInterval(interval);
    }
  }, 100);
})