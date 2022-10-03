const PROXY_CONFIG = [
  {
    context: [
      "/imagefileuploader",
    ],
    target: "https://localhost:7007",
    secure: false,
    changeOrigin: true
  }
]

module.exports = PROXY_CONFIG;
