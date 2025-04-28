# Sheriff-Mod
Sheriff Mod is an Among Us modification for Windows, which adds a new Crewmate class to the game.
<img src ="Pics/SheriffMod.png" width="1000"></img>

<h3>What does the Sheriff do?</h3>
The Sheriff is able to kill Impostors. If they shoot a Crewmate, they will lose their life instead.
<h3>Additional Features</h3>
<ul>
<li> Visibility of the Sheriff can be set in the lobby game options menu</li>
<li> Playable on public Among Us Servers</li>
<li> Custom server regions to join private servers</li>
</ul>

<h2 id="installation"> Installation </h2>
<ul>
<li>Download the Mod for your specific game version. You are not able to launch the game if the versions do not match.</li>
<li>Make a copy of your game’s root directory (Steam/steamapps/common/Among Us) and rename it to whatever you want (Steam/steamapps/common/Among Us Sheriff Mod) </li>
<li>Extract the content of Among Us Sheriff Mod.zip into the copied folder you created</li>
<li>Open your modded folder and open the Game via Among Us.exe</li>
</ul>
<p>Verifying installation success<p>
<ul>
  <li>Launch the Game via Among Us.exe.
  <li>In the top-left corner, below Among Us version, you should see <em>loaded Sheriff Mod vx.y by Woodi </em>
</ul>
<p>If you don't see this message please take a look at our 
  <a href="#troubleshooting">troubleshooting section</a>.
</p>
 
<h2>Releases and Compatibility</h2>
 
 <table style="width:100%">
  <tr>
    <th>Among Us Version</th>
    <th>Mod Version</th>
    <th>Link</th>
          </tr>
         <tr>
    <td>v2021.6.30s</td>
    <td>v1.24</td>
    <td><a href="https://github.com/Woodi-dev/Among-Us-Sheriff-Mod/releases/download/v1.24_2021.6.30s/Among.Us.Sheriff.Mod.v1.24.v2021.6.30s.zip">Download</></td>
  </tr>
       <tr>
    <td>v2021.6.15s</td>
    <td>v1.23</td>
    <td><a href="https://github.com/Woodi-dev/Among-Us-Sheriff-Mod/releases/download/v1.23_2021.6.15s/Among.Us.Sheriff.Mod.v1.23.v2021.6.15s.zip">Download</></td>
  </tr>
     <tr>
    <td>v2021.5.25.2s</td>
    <td>v1.23</td>
    <td><a href="https://github.com/Woodi-dev/Among-Us-Sheriff-Mod/releases/download/v1.23_2021.5.25.2s/Among.Us.Sheriff.Mod.v1.23.v2021.5.25.2s.zip">Download</></td>
  </tr>
        <tr>
    <td>v2021.5.10s</td>
    <td>v1.23</td>
    <td><a href="https://github.com/Woodi-dev/Among-Us-Sheriff-Mod/releases/download/v1.23_2021.5.10s/Among.Us.Sheriff.Mod.v1.23.v2021.5.10s.zip">Download</></td>
  </tr>
      </tr>
        <tr>
    <td>v2021.4.14s</td>
    <td>v1.23</td>
    <td><a href="https://github.com/Woodi-dev/Among-Us-Sheriff-Mod/releases/download/v1.23_2021.4.14s/Among.Us.Sheriff.Mod.v1.23.v2021.4.14s.zip">Download</></td>
  </tr>
      <tr>
    <td>v2021.4.12s</td>
    <td>v1.23</td>
    <td><a href="https://github.com/Woodi-dev/Among-Us-Sheriff-Mod/releases/download/v1.23_2021.4.12s/Among.Us.Sheriff.Mod.v1.23.v2021.4.12s.zip">Download</></td>
  </tr>


</table>
<details>
  <summary>Changelog</summary>
          <h3>v1.24</h3>
   <ul>
    <li>Added Innersloth mod stamp</li>

   </ul>
        <h3>v1.23</h3>
   <ul>
    <li>Fixed a bug: Kill button visible in meetings</li>

   </ul>
      <h3>v1.22</h3>
   <ul>
    <li>Fixed a bug: Custom Sheriff Settings are not visible if language is not set to english</li>

   </ul>
    <h3>v1.21</h3>
   <ul>
    <li>Fixed a bug: custom server region name is South America</li>

   </ul>
  <h3>v1.2</h3>
   <ul>
    <li>Sheriff no longer can kill Impostors hiding in vents.</li>
    <li>Sheriff no longer can kill if they do not see their target</li>
    <li>Sheriff cannot kill during meeting and tasks</li>
    <li>Sheriff kill cooldown does not count down anymore during tasks</li>
    <li>Bugfix: Show Sheriff option disappears in Game Settings tab</li>
    <li>More stable Net Code (Sometimes players do not become Sheriff)</li>
   </ul>
   <h3>v1.1</h3>
   <ul>
    <li>Added Sheriff kill cooldown option to the game lobby</li>
    <li>Added q shortcut to kill as Sheriff</li>
    <li>Kill distance of Impostor and Sheriff are now the same</li>
    <li>Fixed a bug where the outline of the target disappears (Impostor)</li>
    <li>Several nullpointer bugfixes</li>
   </ul>
</details>
Check out Sheriff Mod on CurseForge: <a href="https://www.curseforge.com/among-us/all-mods/sheriff-mod">https://www.curseforge.com/among-us/all-mods/sheriff-mod</a></br>
Play Sheriff Mod on ESPA: <a href="https://github.com/Woodi-dev/ESPA-Sheriff-Mod">https://github.com/Woodi-dev/ESPA-Sheriff-Mod</a>
<h2>Q&A</h2>
 
<p><b>Can you play Proximity Chat (Crewlink) with it?</b></br>
Yes Crewlink does support Among Us Modifications.</p>
<p><b>Can you get banned for playing on public Servers?</b></br>
At the current state of the game there is no perma ban system for the game. The mod is designed in a way, that it does not send prohibited server requests.
You are also able to join your own custom server to be safe <a href="https://github.com/Impostor/Impostor">(Impostor)</a></p>
<p><b>How can i join a custom server?</b></br>
Go to your game directory and open BepInEx/config/org.bepinex.plugins.SheriffMod.txt. There you can set the hostname or IP of the server. Then set the server region to CUSTOM.</p>
<p><b>Do my friends need to install the mod to play it together?</b></br>
Yes. Every player in the game lobby has to install it.</p>

<h2 id="troubleshooting">Troubleshooting</h2>

<p><b>I can't see <em>loaded</em> message on my game screen</b></br>
<ol>
  <li>Make sure you have followed all the <a href="#installation">installation steps</a>, especially launching the game via the Among Us.exe file</li>
  <li>You might be missing some cpp libs (software libraries used by the mod); please install 
    <a href="https://aka.ms/vs/16/release/vc_redist.x86.exe">visual studio c++</a>
  </li>
</ol>
</p>

<p><b>I can't find my issue.</b></br>
You can <a href="https://github.com/Woodi-dev/Among-Us-Sheriff-Mod/issues/new">raise an issue within GitHub</a> documenting your issue. You will need to be logged into GitHub to do this.
</p>

<h2>License</h2>
<p>This software is distributed under the <b>GNU GPLv3</b> License.
<a href="https://github.com/BepInEx/BepInEx">BepinEx</a> is distributed under <b>LGPL-2.1</b> License. </br>
This mod is not affiliated with Among Us or Innersloth LLC, and the content contained therein is not endorsed or otherwise sponsored by Innersloth LLC. Portions of the materials contained herein are property of Innersloth LLC. © Innersloth LLC.</p>

