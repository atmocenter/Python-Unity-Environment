

mergeInto(LibraryManager.library,{
      isMobile: function()
    {
      console.log("mobile testing:");
      console.log(Module.SystemInfo);
        return Module.SystemInfo.mobile;
    },
      Detailsch: function()
    {
       console.log("from js w/ love");
    },
});





