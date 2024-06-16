#include <iostream>
#include <string>

class Interabtable {
public:
    virtual void interact() = 0;
};

// Tür Klasse
class Door : public Interabtable {
private:
    bool isOpen;

public:
    	Door() : isOpen(false) {}

        void open() {
            if(!isOpen) {
                isOpen = true;
                std::cout << "Die Tür ist offen" << std::endl;
            }
        }

        void close() {
            if(isOpen) {
                isOpen = false;
                std::cout << "Die Tür ist geschlossen " << std::endl;
            }
        }

        void interact() override {
            if(isOpen) {
                close();
            } else {
                open();
            }
        }
};

class Window : public Interabtable {
private:
    bool isOpen;
public:
    Window() : isOpen(false) {}

    void open() {
        if(!isOpen) {
            isOpen = true;
            std::cout << "Fenster wurde geöffnet " << std::endl;
        }
    }

    void close() {
        if(isOpen) {
            isOpen = false,
            std::cout << "Fenster wurde geschlossen " << std::endl;
        }
    }

    void interact() {
        if(isOpen) {
            close();
        } else {
            close();
        }
    }
};

class Player {
public:
    // referenz auf Interactable Objekt
    void interactWith(Interabtable& obj) {
        obj.interact();
    }
};

int main() {
    // erstellen
    Player player;
    Door door;
    Window window;

    // Interaktionen mit tür
    player.interactWith(door); // öffne tür
    player.interactWith(door); // schließe tür

    // Interaktionen mit fenster
    player.interactWith(window); // öffne fenster
    player.interactWith(window); // schließe fenster

    return 0;
}