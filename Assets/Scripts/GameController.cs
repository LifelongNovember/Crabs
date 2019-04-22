using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int[,] gridModel;
    public int[,] grid;
    public GameObject XWinScreen;
    public GameObject OWinScreen;
    public GameObject TieWinScreen;
    public bool stillPlaying;

    void Start() {
        gridModel = new int[3,3] {
            {0, 1, 2},
            {5, 4, 3},
            {6, 7, 8}
        };

        grid = new int[3,3] {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };
        stillPlaying = true;
    }

    public void insert(bool isX, int position) {
        int content = isX ? 1 : -1;
        switch(position) {
            case 0:
                grid[0,0] = content;
                break;
            case 1:
                grid[0,1] = content;
                break;
            case 2:
                grid[0,2] = content;
                break;
            case 3:
                grid[1,2] = content;
                break;
            case 4:
                grid[1,1] = content;
                break;
            case 5:
                grid[1,0] = content;
                break;
            case 6:
                grid[2,0] = content;
                break;
            case 7:
                grid[2,1] = content;
                break;
            case 8:
                grid[2,2] = content;
                break;
            default:
                Debug.Log("Grid insertion error");
                break;
        }
    }

    public bool checkIfOver() {
        int resultL = 0;
        int resultC = 0;
        int resultD1 = 0;
        int resultD2 = 0;
        bool isTie = true;
        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                resultL += grid[i,j];
                resultC += grid[j,i];
                if(grid[i,j] == 0) isTie = false;
            }
            if(resultL == 3 || resultC == 3) {
                isOver(true);
                return true;
            }
            else if(resultL == -3 || resultC == -3) {
                isOver(false);
                return true;
            }
            resultL = 0;
            resultC = 0;
        }
        resultD1 = grid[0,0] + grid[1,1] + grid[2,2];
        resultD2 = grid[0,2] + grid[1,1] + grid[2,0];
        if(resultD1 == 3 || resultD2 == 3) {
            isOver(true);
            return true;
        }
        else if(resultD1 == -3 || resultD2 == -3) {
            isOver(false);
            return true;
        }
        if(isTie) {
            isTieOver();
            return true;
        }
        return false;
    }

    public void isOver(bool isX) {
        stillPlaying = false;
        if(isX) XWinScreen.SetActive(true);
        else OWinScreen.SetActive(true);
    }

    public void isTieOver() {
        stillPlaying = false;
        TieWinScreen.SetActive(true);
    }

    
}
