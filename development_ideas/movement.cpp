#include <iostream>
#include <conio.h>

// soll für die späteren variablen in UE5 sein, das ist dann vor allem für D wichtig
const int GRID_WIDTH = 0;
const int GRID_HEIGHT = 0;

struct Player {
    int x;
    int y;
};

int main() {
    Player player;
    // start position
    player.x = 0;
    player.y = 0;

    char input;

    while(true) {

        switch(input) {
            case 'w':
            case 'W':
                if(player.y > 0) {
                    player.y--;
                }
                break;
            case 'a':
            case 'A':
                if(player.x > 0) {
                    player.x--;
                }
                break;
            case 'd':
            case 'D':
                if(player.x < GRID_WIDTH - 1) {
                    player.x++;
                }
                break;
        }
    }

    return 0;
}
