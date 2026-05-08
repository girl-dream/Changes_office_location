会用cmd可以不使用本项目,参考以下步骤或者[视频](https://www.bilibili.com/video/BV14onJzBEid/)

1. 先在自定义的目录创建 `Microsoft Office` 文件夹,名字不能改为别的

2. 用cmd执行以下命令

```bat
mklink /j "C:\Program Files\Microsoft Office" "自定义的目录\Microsoft Office"
```

[阻止OfficePLUS自动安装](https://blog.yerong.org/archives/641)
```bat
reg add "HKLM\SOFTWARE\Microsoft\MSOfficePLUS" /v "InstallSuccess" /f
```

<details>
  <summary>233</summary>
    本来用WinUI写完了,编译exe后带一堆dll后又用winform了
</details>
