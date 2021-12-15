# KMtronic One Channel USB Relay control example
![product_377-500x500](https://user-images.githubusercontent.com/13836042/146020121-a9e9f9c2-6f32-40dc-99d6-f68624ed4116.jpg)  
## Commands:  
1. OFF command: FF 01 00 (HEX) or 255 1 0 (DEC)  
2. ON command: FF 01 01 (HEX) or 255 1 1 (DEC)  
3. Status request command: FF 01 03 (HEX) or 255 1 3 (DEC)  
+ FF 01 xx - Reply from relay where xx is status: 01 - Relay is ON, 00 - Relay is OFF  
