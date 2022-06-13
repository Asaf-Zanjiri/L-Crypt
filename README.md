# L-Crypt
This is my first experience making a crypter. This software gets .Net EXE file and outputs .cs uncompiled stub code
This project is still a WIP. I'm working on it whenever I have free time. In the future i'll try to improve the detection rate.

# Update
Currently working on my own implementations of process-hollowing

# Detections
I've tested it using antiscan.me with r77 rootkit compiled executable.

Before Crypt:

![image](https://user-images.githubusercontent.com/60044819/116005237-bfa5d000-a60e-11eb-8dad-77ded8dd7f92.png)

After Crypt:

![image](https://user-images.githubusercontent.com/60044819/116005246-c92f3800-a60e-11eb-901e-11b5657a77e6.png)

# Credits
- XenCrypt Project - Inspiration for how the crypter should work.
- gigajew - Reflective Loading/Load Assembly code to run the exe in memory.
- Internet Articles - For knowledge on the topic and simple evading methods ideas.
