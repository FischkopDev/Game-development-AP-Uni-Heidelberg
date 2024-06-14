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
            } else {
                std::cout << "Die Tür ist schon offen " << std::endl;
            }
        }

        void close() {
            if(isOpen) {
                isOpen = false;
                std::cout << "Die Tür ist geschlossen " << std::endl;
            } else {
                std::cout << "Die Tür ist bereits geschlossen " << std::endl;
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