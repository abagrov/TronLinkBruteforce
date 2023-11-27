## Console app to restore TronLink wallet password.

## Starting
* Check version of your chrome [chrome://settings/help](chrome://settings/help). 
* Download chromedriver (chromedriver for Chrome 119 already included in repo) from [here](https://chromedriver.chromium.org/downloads) or [here](https://googlechromelabs.github.io/chrome-for-testing/) for your version of chrome. Place it near executable.
* Prepare file with passwords. Passwords should be delimited with new line.
* Run executable with specified arguments.

## Arguments
| Argument            | Description                                 |
|---------------------|------------------------------------------|
|   -c, --chrome-path | Required. Set path to chrome executable. |
|  -u, --user-data-dir | Required. Set path to chrome user data dir. |
|   -p, --passwords-path | Required. Set path to file with passwords. Passwords should be delimited with new line. |
|   -d, --delay | Required. Set delay between actions (text input, click, etc). |
|   --backspace-delay | Required. Set delay between backspace clicks. |
|   --use-ctrl-a | Instructs to use Ctrl + A + Backspace to delete input content. |
|   --kill | Instructs to kill all chrome processes before start. |
|   -c, --chrome-path | Required. Set path to chrome executable. |
|   -c, --chrome-path | Required. Set path to chrome executable. |

Example of work (with big delays due to low perfomance of host). </br>
![Proof of work](https://s5.gifyu.com/images/SRZmw.gif)


