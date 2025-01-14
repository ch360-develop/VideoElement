var hls = {
  HLS_mallocUTF8: function (str) {
    var size = lengthBytesUTF8(str) + 1
    var ptr = _malloc(size)
    stringToUTF8(str, ptr, size)
    return ptr
  },

  HLS_init: function (funcPtr) {
    Module["HLS"] = Module["HLS"] || {}
    Module["HLS"].callbackPtr = funcPtr
  },

  HLS_isSupported: function () {
    if (window.Hls) {
      return Hls.isSupported()
    }
    return false
  },

  HLS_newInstance__deps: ["HLS_mallocUTF8"],
  HLS_newInstance: function (config) {
    if (config) {
      config = JSON.parse(UTF8ToString(config))
    }
    var id = crypto.randomUUID()
    Module["HLS"].instances = Module["HLS"].instances || {}
    Module["HLS"].instances[id] = {
      hls: new Hls(config),
      events: {}
    }
    return _HLS_mallocUTF8(id)
  },

  HLS_destroy: function (id) {
    id = UTF8ToString(id)
    Module["HLS"].instances[id].hls.destroy()
    delete Module["HLS"].instances[id]
  },

  HLS_loadSource: function (id, src) {
    id = UTF8ToString(id)
    src = UTF8ToString(src)
    Module["HLS"].instances[id].hls.loadSource(src)
  },

  HLS_attachMediaById: function (id, videoId) {
    id = UTF8ToString(id)
    videoId = UTF8ToString(videoId)
    var video = document.getElementById(videoId)
    if (!video) {
      return false
    }
    Module["HLS"].instances[id].hls.attachMedia(video)
    return true
  },

  HLS_attachMediaBySelector: function (id, selector) {
    id = UTF8ToString(id)
    selector = UTF8ToString(selector)
    var video = document.querySelector(selector)
    if (!video) {
      return false
    }
    Module["HLS"].instances[id].hls.attachMedia(video)
    return true
  },

  HLS_events_on__deps: ["HLS_mallocUTF8"],
  HLS_events_on: function (_id, _eventName) {
    var id = UTF8ToString(_id)
    var eventName = UTF8ToString(_eventName)
    Module["HLS"].instances[id].events[eventName] = (_event, data) => {
      var idPtr = _HLS_mallocUTF8(id)
      var eventNamePtr = _HLS_mallocUTF8(eventName)
      var dataPtr = _HLS_mallocUTF8(JSON.stringify(data))

      Module.dynCall_viii(Module["HLS"].callbackPtr, idPtr, eventNamePtr, dataPtr)

      _free(idPtr)
      _free(eventNamePtr)
      _free(dataPtr)
    }
    Module["HLS"].instances[id].hls.on(Hls.Events[eventName], Module["HLS"].instances[id].events[eventName])
  },

  HLS_events_off: function (id, eventName) {
    id = UTF8ToString(id)
    Module["HLS"].instances[id].hls.off(Hls.Events[eventName], Module["HLS"].instances[id].events[eventName])
    delete Module["HLS"].instances[id].events[eventName]
  },

  HLS_events_once__deps: ["HLS_mallocUTF8"],
  HLS_events_once: function (_id, _eventName) {
    var id = UTF8ToString(_id)
    var eventName = UTF8ToString(_eventName)
    var callback = (_event, data) => {
      var idPtr = _HLS_mallocUTF8(id)
      var eventNamePtr = _HLS_mallocUTF8(eventName)
      var dataPtr = _HLS_mallocUTF8(JSON.stringify(data))

      Module.dynCall_viii(Module["HLS"].callbackPtr, idPtr, eventNamePtr, dataPtr)

      _free(idPtr)
      _free(eventNamePtr)
      _free(dataPtr)
    }
    Module["HLS"].instances[id].hls.once(Hls.Events[eventName], callback)
  },
}

mergeInto(LibraryManager.library, hls)
