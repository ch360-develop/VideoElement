var videoPlayer = {
  VideoPlayer_mallocUTF8: function (str) {
    var size = lengthBytesUTF8(str) + 1
    var ptr = _malloc(size)
    stringToUTF8(str, ptr, size)
    return ptr
  },

  VideoPlayer_createInstance: function (id, video) {
    Module["VideoPlayer"].instances = Module["VideoPlayer"].instances || {}
    Module["VideoPlayer"].instances[id] = {
      elm: video,
      events: {},
      videoFrameUpdated: false,
      textureWidth: 0,
      textureHeight: 0,
    }

    var ticker = () => {
      Module["VideoPlayer"].instances[id].videoFrameUpdated = true
      video.requestVideoFrameCallback(ticker)
    }
    video.requestVideoFrameCallback(ticker)
  },

  VideoPlayer_init: function (funcPtr) {
    Module["VideoPlayer"] = Module["VideoPlayer"] || {}
    Module["VideoPlayer"].callbackPtr = funcPtr
  },

  VideoPlayer_createVideoElement__deps: ["VideoPlayer_createInstance"],
  VideoPlayer_createVideoElement: function (id) {
    id = UTF8ToString(id)
    var video = document.createElement("video")
    video.id = id
    document.body.appendChild(video)

    _VideoPlayer_createInstance(id, video)
  },

  VideoPlayer_attachVideoElementById__deps: ["VideoPlayer_createInstance"],
  VideoPlayer_attachVideoElementById: function (id) {
    id = UTF8ToString(id)
    var video = document.getElementById(id)
    if (video) {
      return false
    }

    _VideoPlayer_createInstance(id, video)
    return true
  },

  VideoPlayer_attachVideoElementBySelector__deps: ["VideoPlayer_createInstance", "VideoPlayer_mallocUTF8"],
  VideoPlayer_attachVideoElementBySelector: function (selector) {
    selector = UTF8ToString(selector)
    var video = document.querySelector(selector)
    if (video) {
      return null
    }

    var id = crypto.randomUUID()
    _VideoPlayer_createInstance(id, video)
    return _VideoPlayer_mallocUTF8(id)
  },

  VideoPlayer_removeElement: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      Module["VideoPlayer"].instances[id].elm.remove()
      delete Module["VideoPlayer"].instances[id]
    }
  },

  VideoPlayer_addEventListener__deps: ["VideoPlayer_mallocUTF8"],
  VideoPlayer_addEventListener: function (_id, _eventName) {
    var id = UTF8ToString(_id)
    if (Module["VideoPlayer"].instances[id]) {
      var video = Module["VideoPlayer"].instances[id].elm
      var eventName = UTF8ToString(_eventName)
      Module["VideoPlayer"].instances[id].events[eventName] = () => {
        var idPtr = _VideoPlayer_mallocUTF8(id)
        var eventNamePtr = _VideoPlayer_mallocUTF8(eventName)

        Module.dynCall_vii(Module["VideoPlayer"].callbackPtr, idPtr, eventNamePtr)

        _free(idPtr)
        _free(eventNamePtr)
      }
      video.addEventListener(eventName, Module["VideoPlayer"].instances[id].events[eventName])
    }
  },

  VideoPlayer_removeEventListener: function (id, eventName) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      var video = Module["VideoPlayer"].instances[id].elm
      eventName = UTF8ToString(eventName)
      video.removeEventListener(eventName, Module["VideoPlayer"].instances[id].events[eventName])
      delete Module["VideoPlayer"].instances[id].events[eventName]
    }
  },

  VideoPlayer_canPlayType: function (id, type) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      type = UTF8ToString(type)
      return Module["VideoPlayer"].instances[id].elm.canPlayType(type)
    }
    return false
  },

  VideoPlayer_setSource: function (id, uri) {
    uri = UTF8ToString(uri)
    if (Module["VideoPlayer"].instances[id]) {
      var video = Module["VideoPlayer"].instances[id].elm
      video.src = uri
    }
  },

  VideoPlayer_play: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      Module["VideoPlayer"].instances[id].elm.play()
    }
  },

  VideoPlayer_pause: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      Module["VideoPlayer"].instances[id].elm.pause()
    }
  },

  VideoPlayer_getCurrentTime: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.currentTime
    }
    return NaN
  },

  VideoPlayer_setCurrentTime: function (id, time) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      Module["VideoPlayer"].instances[id].elm.currentTime = time
    }
  },

  VideoPlayer_getDuration: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.duration
    }
    return NaN
  },

  VideoPlayer_getMuted: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.muted
    }
    return false
  },

  VideoPlayer_setMuted: function (id, muted) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      Module["VideoPlayer"].instances[id].elm.muted = muted
    }
  },

  VideoPlayer_getReadyState: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.readyState
    }
    return 0
  },

  VideoPlayer_getEnded: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.ended
    }
    return false
  },

  VideoPlayer_getSeekable: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.seekable
    }
    return false
  },

  VideoPlayer_getLoop: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.loop
    }
    return false
  },

  VideoPlayer_setLoop: function (id, value) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      Module["VideoPlayer"].instances[id].elm.loop = value
    }
  },

  VideoPlayer_getVolume: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.volume
    }
    return false
  },

  VideoPlayer_setVolume: function (id, value) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      Module["VideoPlayer"].instances[id].elm.volume = value
    }
  },

  VideoPlayer_getVideoHeight: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.videoHeight
    }
    return 0
  },

  VideoPlayer_getVideoWidth: function (id) {
    id = UTF8ToString(id)
    if (Module["VideoPlayer"].instances[id]) {
      return Module["VideoPlayer"].instances[id].elm.videoWidth
    }
    return 0
  },

  VideoPlayer_createVideoTexture: function (tex) {
    var t = GLctx.createTexture()
    GL.textures[tex] = t
    GL.textures[tex].name = tex
  },

  VideoPlayer_deleteVideoTexture: function (tex) {
    GLctx.deleteTexture(GL.textures[tex])
  },

  VideoPlayer_fetchVideoTexture: function (id, tex) {
    id = UTF8ToString(id)
    if (!Module["VideoPlayer"].instances[id] || !Module["VideoPlayer"].instances[id].videoFrameUpdated) {
      return false
    }

    var video = Module["VideoPlayer"].instances[id].elm
    var prevTex = GLctx.getParameter(GLctx.TEXTURE_BINDING_2D)
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[tex])
    GLctx.texParameteri(GLctx.TEXTURE_2D, GLctx.TEXTURE_WRAP_S, GLctx.CLAMP_TO_EDGE)
    GLctx.texParameteri(GLctx.TEXTURE_2D, GLctx.TEXTURE_WRAP_T, GLctx.CLAMP_TO_EDGE)
    GLctx.texParameteri(GLctx.TEXTURE_2D, GLctx.TEXTURE_MIN_FILTER, GLctx.LINEAR)
    GLctx.texImage2D(GLctx.TEXTURE_2D, 0, GLctx.RGBA, GLctx.RGBA, GLctx.UNSIGNED_BYTE, video)
    GLctx.bindTexture(GLctx.TEXTURE_2D, prevTex)

    Module["VideoPlayer"].instances[id].videoFrameUpdated = false
    Module["VideoPlayer"].instances[id].textureWidth = video.videoWidth
    Module["VideoPlayer"].instances[id].textureHeight = video.videoHeight

    return true
  },
}

mergeInto(LibraryManager.library, videoPlayer)
