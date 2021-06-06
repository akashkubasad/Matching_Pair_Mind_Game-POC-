// Matching Pair Game Approach and Structure
// v1.0.0

// CardGenerator is a Non Monobehaviour class the is retuens an array of cards list with pairs. 
// Game with (m * n) elements. if Odd elements shows error message, on even elements creates a grid.
// 2 Enumerables. unshuffled enumerable containing range from (1-N) elements.
// in a loop fetch random index from the unshuffled enumerable and store it in shuffled list till last element without repeating.
// Shuffled list is ready with N length of random index.
//
// Initialise an array of Cards with (m * n) capacity. 
// Total pairs should me N/2.
// create a class that stores the properties of Card. i.e CardId and Color.
// the game uses intiger value as index. i.e. from 0-totalPairs.
// in a loop from 0-totalPairs, create an object of Card Class.
// set current index and random color to the object.
// store it in the Cards Array for the indices, i.e. (index *2) - 1 and (index*2) - 2.

// Generated an array of cards with CardId and Color, with radom index for the (m * n) elements.

// Grid Controller is MonoBehavious class that creates an object of type cardGenerator.
// cardgenerator objects returns an array of cards.
// Instantiate cards in the grid and initialise the data with the array of cards datum. 


// CardElement is an Abstract class that implements IFlip, IClick,ICard.
// GameCard gives implementation of abstract class for all the functioalities.

// when a card is clicked it registers, card registers instance in the stack of cards. that is managed by MatchingPairLogic Mono Class.
// Enqueue each clicks.
// on even "Even" total clicks Dequeue last 2 elements
// if both of them match increments total pairedmatch value and compare with the total pairs in the grid.
// on wrong match, Dequeue the last cards and call Call IFlip implementation of the cards, on the next click.
// on all pairs matched. Gameover is called and displays the totals clicks to solve the puzzle and time taken.

