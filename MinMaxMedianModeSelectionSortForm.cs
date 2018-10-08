using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace SelectionSort
{
    public partial class MinMaxMedianModeSelectionSortForm : Form
    {
        // All your code should be placed within the class.

        #region Examples of Comments
        // This is a one-line comment

        /*
         * 
         * This is a multi-line comment.
         * 
         */
        #endregion

        #region Global Declarations, Initializations and Instantiations
        // Global variables and objects should be created at the top of the class.
        // If a variable or object is global to a class, any procedure in the class
        // can access and modify it.

        // Declaration: Associates a name with a data type or class
        // Initialization: Gives a variable its initial value
        // Instantiation: Creates an object and initializes it

        /*
         * The part of the following statement that lies to the left of the equals sign 
         * associates the name "numberList" with a list of "double" values. This is called 
         * a "declaration." The part that lies on the right creates the initially empty list. 
         * This is called an "instantiation."
         */
        private List<double> numberList = new List<double>();

        #endregion

        #region A Note on Object-Oriented Programming
        /*
         * In object-oriented programming, classes are the "blueprints" or "templates" for
         * making objects. Just as a cookie-cutter can be used to make any number of cookies,
         * a class can be used to make any number of objects. In Microsoft Visual Studio,
         * objects consist of properties, methods and events.
         * 
         * property-> A variable that belongs to an object, typically used to store
         *            information about a characteristic of the object
         * method  -> A procedure or function that belongs to an object. Methods are
         *            ACTIONS that can be performed by, to or for an object.
         * event   -> An event is a message sent by an object to signal the occurrence 
         *            of an action. Events are used to trigger the execution of code.
         * 
         * For example, the "Button" class is used to make buttons in Windows Forms
         * applications. 
         * 
         * Text    -> A property found within the Button class. It stores the text that is 
         *            displayed on the button.
         * Hide    -> A method found within the Button class. It is used to hide a button.
         * Click   -> An event found within the Button class. The "Click" event is is raised
         *            whenever the user clicks on a button.
         */
        #endregion

        #region Constructor Method
        // The following is called a "constructor method" for the form "SelectionSortForm."
        // Place within the constructor method any code that needs to be executed as 
        // soon as the form is created (i.e. loaded). 
        public MinMaxMedianModeSelectionSortForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            double number;
            bool isNumeric = Double.TryParse(numberTextBox.Text, out number);

            numberListBox.ClearSelected(); //Remove any highlighting

            if (isNumeric) // This is equivalent to 'if (isnumeric == true)'
            {
                // Add the entered number to both the list and the list box
                numberList.Add(number);
                numberListBox.Items.Add(number);
            }
            else
            {
                MessageBox.Show("You may only enter numbers.", "Who was your kindergarten teacher?");
            }
            numberTextBox.Text = "";

        }

        private void MinButton_Click(object sender, EventArgs e)
        {
            numberListBox.ClearSelected();
            List<double> copyList = new List<double>(numberList);

            if (!isListEmpty())
            {
                displayOutput(MinNumber(copyList, true).value, "minimum value");
            }



        }

        private void MaxButton_Click(object sender, EventArgs e)
        {
            numberListBox.ClearSelected();
            List<double> copyList = new List<double>(numberList);

            if (!isListEmpty())
            {
                displayOutput(MaxNumber(copyList).value, "maximum value");
            }

        }

        // Used to ensure that the focus stays on the text box when the button is clicked.
        // Otherwise, the button will have the focus.
        private void SubmitButton_MouseUp(object sender, MouseEventArgs e)
        {
            numberTextBox.SelectAll();
            numberTextBox.Focus();
        }

        private void MeanButton_Click(object sender, EventArgs e)
        {
            numberListBox.ClearSelected();
            List<double> copyList = new List<double>(numberList);

            if (!isListEmpty())
            {
                displayOutput(Mean(copyList), "mean");
            }

        }

        private void ModeButton_Click(object sender, EventArgs e)
        {
            numberListBox.ClearSelected();
            List<double> copyList = new List<double>(numberList);

            if (!isListEmpty())
            {
                displayOutput(Mode(copyList));
            }
        }

        private void ScrambleButton_Click(object sender, EventArgs e)
        {
            numberListBox.ClearSelected();
            List<double> copyList = new List<double>(numberList);

            if (!isListEmpty())
            {
                displayOutput(ScrambleList(copyList), "scrambled list");
            }

        }

        private void MedianButton_Click(object sender, EventArgs e)
        {
            numberListBox.ClearSelected();
            List<double> copyList = new List<double>(numberList);

            if (!isListEmpty())
            {
                displayOutput(Median(copyList), "median");
            }
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            numberListBox.ClearSelected();
            List<double> copyList = new List<double>(numberList);

            if (AscendingRadioButton.Checked)
            {
                if (!isListEmpty())
                {
                    //displayOutput(Accending(copyList, true), "accending list");
                    //displayOutput(SwapSort(copyList), "accending list");
                    //displayOutput(InsertionSort(copyList), "accending list");
                    displayOutput(CocktailSort(copyList), "accending list");
                }
            }
            else if (DescendingRadioButton.Checked)
            {
                if (!isListEmpty())
                {
                    displayOutput(Descending(copyList), "descending list");
                }
            }
        }



        private void SearchButton_Click(object sender, EventArgs e)
        {
            numberListBox.ClearSelected();
            double searchNum;
            bool isNumeric = Double.TryParse(searchTextBox.Text, out searchNum);
            List<double> copyList = new List<double>(numberList);


            if (!isListEmpty() & isNumeric == true)
            {
                SearchInfo searchResults = new SearchInfo();
                searchResults = SearchListBinary(copyList, searchNum);
                numberListBox.ClearSelected();
                HighlightItems(searchResults.index);
                if (searchResults.numReoccurrence != -1)
                {
                    displayOutput(searchResults.numReoccurrence, searchResults.index);
                }
                else
                {
                    DisplaySearchNotFound();
                }
                searchTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Type a number to search for", "ERROR");
            }
        }

        private void RandomListButton_Click(object sender, EventArgs e)
        {
            numberListBox.ClearSelected();
            RandomList();
        }

        private void PrimeButton_Click(object sender, EventArgs e)
        {
            double primeCheckDouble = new double();
            Double.TryParse(numberTextBox.Text, out primeCheckDouble);
            int primeCheckInt = Convert.ToInt32(primeCheckDouble);
            if (PrimeNumber(primeCheckInt))
            {
                MessageBox.Show("You have entered a prime number.");
            }
            else
            {
                MessageBox.Show("The number you entered is not prime.");
            }

        }
        #endregion

        #region Programmer-Defined Methods

        //Check if list is empty

        private bool isListEmpty()
        {
            if (numberList.Count == 0)
            {
                MessageBox.Show("You need to add numbers to the list", "ERROR");
                return true;
            }
            return false;
        }

        private void HighlightItems(List<int> highlightIndex)
        {

            numberListBox.ClearSelected();
            for (int i = 0; i < highlightIndex.Count; i++)
            {
                numberListBox.SelectedIndex = highlightIndex[i];
            }

        }

        //Display Output

        /// <summary>
        /// Displays the output in a pop-up box. This function is used for displaying single values along with a description of the value.
        /// </summary>
        /// <param name="display">The value that you want displayed.</param>
        /// <param name="message">A discription of what the value is.</param>
        private void displayOutput(double display, string message)
        {
            MessageBox.Show("The " + message + " is: " + display);
        }

        /// <summary>
        /// Displays the output in a pop-up box. This function is used for displaying a list along with a description of the list.
        /// </summary>
        /// <param name="display">The list that you want displayed.</param>
        /// <param name="message">A discription of what the list is.</param>
        private void displayOutput(List<double> display, string message)
        {
            string displayText = String.Join(", ", display);
            MessageBox.Show("The " + message + " is: " + displayText);
        }

        /// <summary>
        /// Displays the output in a pop-up box. This function is used for displaying the results of a search.
        /// </summary>
        /// <param name="numReoccurrence">Number of reoccurrence of the searched value.</param>
        /// <param name="index">The indexs of the searched value.</param>
        private void displayOutput(int numReoccurrence, List<int> index)
        {
            List<double> searchKeySpotInList = new List<double>();
            for (int i = 0; i < index.Count; i++)
            {
                index[i] += 1;

                searchKeySpotInList.Add(Convert.ToDouble(index[i]));
            }

            searchKeySpotInList = Accending(searchKeySpotInList, false);

            string displayText = String.Join(", ", searchKeySpotInList);
            MessageBox.Show("The number appears " + numReoccurrence + " times." + System.Environment.NewLine + "The number appears at these spots: " + displayText);
        }

        /// <summary>
        /// Displays the output in a pop-up box. This function is used for displaying the results of mode.
        /// </summary>
        /// <param name="modeValue">Mode value.</param>
        /// <param name="numReoccurrence">Number reoccurrence of the mode.</param>
        private void displayOutput(ModeInfo modeData)
        {
            List<double> modeValue = new List<double>(modeData.value);
            int numReoccurrence = modeData.numRecurrences;

            string displayText = String.Join(", ", modeValue);
            MessageBox.Show("the mode is " + displayText + System.Environment.NewLine + "The mode occurs " + numReoccurrence + " times");
        }

        /// <summary>
        /// Displays that the number being search for could not be found in the list.
        /// </summary>
        private void DisplaySearchNotFound()
        {
            MessageBox.Show("The number you searched for could not be found");
        }

        //Display output in ListBox

        /// <summary>
        /// Displays a list inside the list box
        /// </summary>
        /// <param name="displayList">The last that will be shown in the list box.</param>
        private void displayListInBox(List<double> displayList)
        {
            List<double> copyList = new List<double>(displayList);
            numberListBox.Items.Clear();
            numberList.Clear();
            numberList = copyList;
            for (int i = 0; i < copyList.Count; i++)
            {
                numberListBox.Items.Add(copyList[i]);
            }
        }


        //Find Minimum Number and Index

        /// <summary>
        /// Finds the minimum value in a list of doubles.
        /// </summary>
        /// <returns>The minimum value.</returns>
        /// <param name="originalList">The list that you want to find the minimum value in.</param>
        private Number MinNumber(List<double> originalList, bool highlightMin)
        {
            double smallestValue = originalList[0];
            int smallestIndex = 0;
            List<int> listIndexs = new List<int>();

            Dictionary<int, double> smallestInfo = new Dictionary<int, double>();

            for (int i = 1; i < originalList.Count; i++)
            {
                if (originalList[i] <= smallestValue)
                {
                    smallestValue = originalList[i];
                    smallestIndex = i;
                    //numberListBox.SelectedItem = i;
                }
            }

            Number minNumber = new Number();
            minNumber.value = smallestValue;
            minNumber.index = smallestIndex;
            if (highlightMin)
            {
                numberListBox.SelectedIndex = minNumber.index;
            }
            return (minNumber);

        }

        //Find Maximum Number and Index

        /// <summary>
        /// Finds the maximum number in a list.
        /// </summary>
        /// <returns>The maximum value.</returns>
        /// <param name="originalList">The list that you want to find the maximum value in.</param>
        private Number MaxNumber(List<double> originalList)
        {
            double largestValue = originalList[0];
            int largestIndex = 0;
            Dictionary<int, double> smallestInfo = new Dictionary<int, double>();

            for (int i = 1; i < originalList.Count; i++)
            {
                if (originalList[i] > largestValue)
                {
                    largestValue = originalList[i];
                    largestIndex = i;
                }
            }

            Number maxNumber = new Number();
            maxNumber.value = largestValue;
            maxNumber.index = largestIndex;

            numberListBox.SelectedIndex = maxNumber.index;

            return (maxNumber);

        }

        //Find the Mean

        /// <summary>
        /// Finds the mean of a list
        /// </summary>
        /// <returns>The mean of the list.</returns>
        /// <param name="originalList">The list that you want to find the mean of.</param>
        private double Mean(List<double> originalList)
        {
            double sum = 0;
            double mean;

            for (int i = 0; i < originalList.Count; i++)
            {
                sum += originalList[i];
            }

            mean = sum / originalList.Count;
            return (mean);
        }

        private List<double> InsertionSort(List<double> originalList)
        {
            List<double> sortedList = new List<double>(originalList);
            for (int i = 1; i < sortedList.Count; i++)
            {
                double insertionNum = sortedList[i];
                int insertionIndex = i - 1;
                while (insertionNum >= 0 && sortedList[insertionIndex] > insertionNum)
                {
                    sortedList[insertionIndex + 1] = sortedList[insertionIndex];
                    insertionIndex--;
                }
                sortedList[insertionIndex + 1] = insertionNum;
            }

            return sortedList;
        }

        //Sort Accending
        private List<double> CocktailSort(List<double> unsortedList)
        {
            List<double> sortedList = new List<double>(unsortedList);
            int start = 0, end = unsortedList.Count - 1;
            bool sorted = false;

            while (!sorted)
            {
                sorted = true;
                for (int i = start; i >= start && i < end; i++)
                {
                    if (sortedList[i] > sortedList[i + 1])
                    {
                        double swapNum = sortedList[i + 1];
                        sortedList.RemoveAt(i + 1);
                        sortedList.Insert(i, swapNum);
                        sorted = false;
                    }
                }
                end--;

                for (int i = end - 1; i < end && i >= start; i--)
                {
                    if (sortedList[i] > sortedList[i + 1])
                    {
                        double swapNum = sortedList[i + 1];
                        sortedList.RemoveAt(i + 1);
                        sortedList.Insert(i, swapNum);
                        sorted = false;
                    }
                }
                start++;
            }
            return sortedList;
        }

        /// <summary>
        /// Displays the accending list using swap sort.
        /// </summary>
        /// <returns>The sorted list in accending.</returns>
        /// <param name="originalList">The list that you want sorted.</param>
        private List<double> SwapSort(List<double> originalList)
        {
            List<double> sortedList = new List<double>(originalList);
            double swapNum = new double();
            int index = 0;

            while (index < originalList.Count)
            {
                Console.WriteLine(index);
                if (index == 0 || sortedList[index] >= sortedList[index - 1])
                {
                    index++;
                }
                else
                {
                    swapNum = sortedList[index];
                    sortedList.RemoveAt(index);
                    sortedList.Insert(index - 1, swapNum);
                    index--;
                }

            }
            displayListInBox(sortedList);
            return sortedList;
        }

        /// <summary>
        /// Sorts a list in accending order.
        /// </summary>
        /// <returns>The accending list.</returns>
        /// <param name="originalList">The list that will be sorted in accending order.</param>
        private List<double> Accending(List<double> originalList, bool displayInListBox)
        {
            List<double> accendingList = new List<double>();

            while (originalList.Count > 0)
            {
                Number minNum = MinNumber(originalList, displayInListBox);
                accendingList.Add(minNum.value);
                originalList.RemoveAt(minNum.index);
            }
            if (displayInListBox)
            {
                displayListInBox(accendingList);
            }
            return accendingList;
        }


        //Sort Deccending

        /// <summary>
        /// Sorts a list in descending order.
        /// </summary>
        /// <returns>The The descending list.</returns>
        /// <param name="originalList">The list that will be sorted in descending order.</param>
        private List<double> Descending(List<double> originalList)
        {
            List<double> descendingList = new List<double>();

            while (originalList.Count > 0)
            {
                Number maxNum = MaxNumber(originalList);
                descendingList.Add(maxNum.value);
                originalList.RemoveAt(maxNum.index);
            }
            displayListInBox(descendingList);
            return descendingList;
        }

        //Find the Mode

        /// <summary>
        /// Finds the mode of a list.
        /// </summary>
        /// <returns>The mode of the list.</returns>
        /// <param name="originalList">The list that you want to find the mode of.</param>
        private ModeInfo Mode(List<double> originalList)
        {
            ModeInfo mode = new ModeInfo();
            mode.numRecurrences = 0;
            mode.value = new List<double>();

            List<double> sortedList = Accending(originalList, false);
            List<double> modeValue = new List<double>();
            int numRecurrences = 1;
            double modeCheck = sortedList[0];

            for (int i = 1; i < sortedList.Count; i++)
            {
                if (modeCheck == sortedList[i])
                {
                    numRecurrences++;
                }
                else
                {
                    if (numRecurrences > mode.numRecurrences)
                    {
                        mode.value.Clear();
                        mode.value.Add(modeCheck);
                        mode.numRecurrences = numRecurrences;
                    }
                    else if (numRecurrences == mode.numRecurrences)
                    {
                        mode.value.Add(modeCheck);
                    }
                    modeCheck = sortedList[i];
                    numRecurrences = 1;
                }
            }
            if (numRecurrences > mode.numRecurrences)
            {
                mode.value.Clear();
                mode.value.Add(modeCheck);
                mode.numRecurrences = numRecurrences;
            }
            else if (numRecurrences == mode.numRecurrences)
            {
                mode.value.Add(modeCheck);
            }
            numberListBox.ClearSelected();
            return mode;
        }

        //Create a Scrambled List

        /// <summary>
        /// Arranges a list in a random order.
        /// </summary>
        /// <returns>The scrambled list.</returns>
        /// <param name="originalList">The list that you want scrambled.</param>
        private List<double> ScrambleList(List<double> originalList)
        {
            List<double> scrambleList = new List<double>();
            Random rnd = new Random();
            int randomIndex = new int();

            while (originalList.Count != 0)
            {
                randomIndex = rnd.Next(0, originalList.Count);
                scrambleList.Add(originalList[randomIndex]);
                originalList.RemoveAt(randomIndex);
            }
            displayListInBox(scrambleList);
            return scrambleList;
        }

        /// <summary>
        /// Finds the median of a list.
        /// </summary>
        /// <returns>The median of the list.</returns>
        /// <param name="originalList">That list that you want to find the median of.</param>
        private double Median(List<double> originalList)
        {
            List<double> accendingList = Accending(originalList, false);
            double median = new double();
            int middleOfListIndex = new int();
            if (accendingList.Count % 2 != 0)
            {
                middleOfListIndex = (int)Math.Floor((float)accendingList.Count / 2);
                median = accendingList[middleOfListIndex];
            }
            else
            {
                List<double> middleOfListValue = new List<double>();
                middleOfListIndex = accendingList.Count / 2;
                middleOfListValue.Add(accendingList[middleOfListIndex]);
                middleOfListValue.Add(accendingList[middleOfListIndex - 1]);
                median = Mean(middleOfListValue);
            }
            return median;
        }

        //Search the List 

        /// <summary>
        /// Searches the list for a specific number.
        /// </summary>
        /// <returns>A variable of type SearchInfo which contains the number of occurrences of the key and the indexs of the key.</returns>
        /// <param name="orginalList">The list that you want to search for the key in.</param>
        /// <param name="key">The value you want to search for.</param>
        private SearchInfo SearchListBinary(List<double> orginalList, double key)
        {
            SearchInfo foundNum = new SearchInfo();
            foundNum.numReoccurrence = 0;

            int middleIndex = orginalList.Count / 2;
            foundNum.index = new List<int>();
            bool isNumFound = false;
            List<double> sortedList = Accending(orginalList, true);
            int maxSearches = Convert.ToInt32(Math.Log(Convert.ToDouble(sortedList.Count), 2.0)) + 2;
            int searchIndex = middleIndex;
            bool numNotInList = false;
            int bottom = 0, top = sortedList.Count - 1;
            while (bottom <= top)
            {
                int middle = (bottom + top) / 2;
                if (key < sortedList[middle])
                {
                    top = middle - 1;
                }
                else if (key > sortedList[middle])
                {
                    bottom = middle + 1;
                }
                else
                {
                    foundNum.numReoccurrence += 1;
                    foundNum.index.Add(middle);
                    isNumFound = true;
                    break;
                }
            }

            if (!isNumFound)
            {
                numNotInList = true;
            }

            /*
            while (!isNumFound & !numNotInList)
            {
                if(sortedList[searchIndex] == key)
                {
                    foundNum.numReoccurrence += 1;
                    foundNum.index.Add(searchIndex);
                    isNumFound = true;
                }
                else if (searchIndex == 1)
                {
                    searchIndex--;
                    if(sortedList[searchIndex] == key)
                    {
                        foundNum.numReoccurrence += 1;
                        foundNum.index.Add(searchIndex);
                        isNumFound = true;
                    }
                    else
                    {
                        searchIndex += 2;
                    }
                }
                else if(sortedList[searchIndex] > key)
                {
                    searchIndex -= middleIndex / 2;
                    middleIndex /= 2;
                }
                else if (sortedList[searchIndex] < key)
                {
                    searchIndex += middleIndex / 2;
                    middleIndex /= 2;
                }
                if(counter > maxSearches)
                {
                    numNotInList = true;
                }
                counter++;

            }
            */

            int stepsToLeft = 1;

            while (isNumFound & !numNotInList)
            {
                if (foundNum.index[0] - stepsToLeft >= 0)
                {
                    if (key == sortedList[foundNum.index[0] - stepsToLeft])
                    {
                        foundNum.numReoccurrence += 1;
                        foundNum.index.Add(foundNum.index[0] - stepsToLeft);
                    }
                    else
                    {
                        isNumFound = false;
                    }
                    stepsToLeft += 1;
                }
                else
                {
                    isNumFound = false;
                }
            }

            isNumFound = true;

            int stepsToRight = 1;

            while (isNumFound & !numNotInList)
            {
                if (foundNum.index[0] + stepsToRight < sortedList.Count)
                {
                    if (key == sortedList[foundNum.index[0] + stepsToRight])
                    {
                        foundNum.numReoccurrence += 1;
                        foundNum.index.Add(foundNum.index[0] + stepsToRight);
                    }
                    else
                    {
                        isNumFound = false;
                    }
                    stepsToRight += 1;
                }
                else
                {
                    isNumFound = false;
                }
            }
            if (!numNotInList)
            {
                return foundNum;
            }
            else
            {
                SearchInfo notfound;
                notfound.numReoccurrence = -1;
                notfound.index = new List<int>();
                return notfound;
            }
        }

        /*
        private SearchInfo SearchList(List<double> originalList, double searchNum, bool isNumeric)
        {
            
            SearchInfo foundNum = new SearchInfo();
            foundNum.numReoccurrence = 0;
            foundNum.index = new List<int>();

            if (isNumeric)
            {
                for (int i = 0; i < originalList.Count; i++)
                {
                    if (searchNum == originalList[i])
                    {
                        foundNum.numReoccurrence++;
                        foundNum.index.Add(i + 1);
                    }

                }
                foreach(int item in foundNum.index)
                {
                    Console.WriteLine("hi");
                    numberListBox.SetSelected(item - 1, true);
                }
            }
            else
            {
                MessageBox.Show("You may only enter numbers.", "Who was your kindergarten teacher?");
            }
            return foundNum;

        }
        */

        // Display a Random List

        /// <summary>
        /// Creates of random list of doubles.
        /// </summary>
        private void RandomList()
        {
            Random rnd = new Random();
            int listLength = rnd.Next(10, 15);
            double randomValue = new double();

            numberListBox.Items.Clear();

            numberList.Clear();

            for (int i = 1; i < listLength; i++)
            {
                randomValue = Math.Round(rnd.NextDouble() * 2000 - 1000, 2);
                numberListBox.Items.Add(randomValue);
                numberList.Add(randomValue);
            }
        }

        #endregion

        #region Computer Science Problems 

        private bool PrimeNumber (int input)
        {
            bool isPrime = true;

            for (int i = 2; i < (input/2); i++)
            {
                if (input % i == 0)
                {
                    isPrime = false;
                }

            }
            return isPrime;
        }

        private void Discriminate(string quadFormula)
        {

        }

        private void LinearEquationSolver(string equation)
        {

        }

        private void DistanceBetweenPoints(string points)
        {

        }




        #endregion

    }// end of class
}// end of namespace
