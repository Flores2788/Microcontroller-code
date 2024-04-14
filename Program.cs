// Constants 
const int RED_LED_PIN = 9;
const int GREEN_LED_PIN = 8;
const int BLUE_LED_PIN = 7;
int[] LEDS = [RED_LED_PIN, GREEN_LED_PIN, BLUE_LED_PIN];

// The program
showSequence(createRandomSequenceOfLength(4, null, LEDS));

// Functions

Node createRandomSequenceOfLength(int length, Node start, int[] values)
{
    start ??= new Node(); // if there is no starting node create it.
    Random rnd = new Random(); 
    Node cNode = start; // set the current node to the starting node
    for (int i = 0; i < length; i++)
    {
        int randomIndex = rnd.Next(0, values.Length); // Pick random array indx 
        cNode.value = values[randomIndex]; // set the value of the current node based on the random index.
        if (cNode.next == null && i + 1 < length) // if we have not created enough nodes, create a new node and insert it in the list.
        {
            Node n = new Node();
            cNode.next = n;
            cNode = n; // the current node is now the new node.
        }
    }
    cNode.next = null; // This might create a memory leak. 
    return start; //Always return the start of the list. 
}

void showSequence(Node n)
{
    Console.WriteLine(n.value);
    if (n.next != null){ showSequence(n.next);}
}

// The class structure that lets us create the lists. 
class Node
{
    public int value;
    public Node next;
}