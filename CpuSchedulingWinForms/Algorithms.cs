using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int numProcesses = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("First Come, First Serve Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTimes = new double[numProcesses];
                double[] waitTimes = new double[numProcesses];
                double[] arrivalTimes = new double[numProcesses];
                double[] completionTimes = new double[numProcesses];
                bool[] completed = new bool[numProcesses];
                string outputString = "";
                double totalTurnaroundTime = 0;

                //collect process information
                for (int i = 0; i < numProcesses; i++)
                {
                    string burstInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    string arrivalInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                           "Arrival time for P" + (i + 1),
                                                           "",
                                                           -1, -1);

                    burstTimes[i] = Convert.ToDouble(burstInput);
                    arrivalTimes[i] = Convert.ToDouble(arrivalInput);
                    completed[i] = false;
                }

                double currentTime = 0;
                int completedProcesses = 0;

                //run until all processes are completed, processing in the order of arrival times
                while (completedProcesses < numProcesses)
                {
                    int firstProcess = -1;
                    double earliestArrival = double.MaxValue;

                    //find the first arriving process that hasn't yet been completed
                    for (int i = 0; i < numProcesses; i++)
                    {
                        if (!completed[i] && arrivalTimes[i] < earliestArrival)
                        {
                            earliestArrival = arrivalTimes[i];
                            firstProcess = i;
                        }
                    }

                    if (firstProcess != -1)
                    {
                        //adjust current time to account for idle time
                        if (currentTime < arrivalTimes[firstProcess])
                        {
                            currentTime = arrivalTimes[firstProcess];
                        }

                        //execute the process
                        waitTimes[firstProcess] = currentTime - arrivalTimes[firstProcess];
                        currentTime += burstTimes[firstProcess];
                        completionTimes[firstProcess] = currentTime;
                        completed[firstProcess] = true;
                        completedProcesses++;
                        totalTurnaroundTime += (completionTimes[firstProcess] - arrivalTimes[firstProcess]);

                        outputString += ("P" + (firstProcess + 1) + " Burst/Waiting/Turnaround Time: "
                                         + burstTimes[firstProcess] + "/" + waitTimes[firstProcess] + "/"
                                         + (completionTimes[firstProcess] - arrivalTimes[firstProcess]) + "\r\n");
                    }
                }

                double totalWaitTime = 0.0;
                for (int i = 0; i < numProcesses; i++)
                {
                    totalWaitTime += waitTimes[i];
                }

                double averageWaitTime = totalWaitTime / numProcesses;
                outputString += "Average Waiting Time = " + averageWaitTime + "\r\n";
                double averageTurnaroundTime = totalTurnaroundTime / numProcesses;
                outputString += "Average Turnaround Time = " + averageTurnaroundTime + "\r\n";

                MessageBox.Show(outputString, "Process Information", MessageBoxButtons.OK);
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int numProcesses = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTimes = new double[numProcesses];
                double[] waitTimes = new double[numProcesses];
                double[] arrivalTimes = new double[numProcesses];
                double[] completionTimes = new double[numProcesses];
                bool[] completed = new bool[numProcesses];
                string outputString = "";
                double totalTurnaroundTime = 0;

                //collect process information
                for (int i = 0; i < numProcesses; i++)
                {
                    string burstInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    string arrivalInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                           "Arrival time for P" + (i + 1),
                                                           "",
                                                           -1, -1);

                    burstTimes[i] = Convert.ToDouble(burstInput);
                    arrivalTimes[i] = Convert.ToDouble(arrivalInput);
                    completed[i] = false;
                }

                double currentTime = 0;
                int completedProcesses = 0;

                //run until all processes are completed
                while (completedProcesses < numProcesses)
                {
                    double shortestBurstTime = double.MaxValue;
                    int shortestProcess = -1;

                    //find the process with the shortest burst time that has arrived
                    for (int i = 0; i < numProcesses; i++)
                    {
                        if (!completed[i] && arrivalTimes[i] <= currentTime && burstTimes[i] < shortestBurstTime)
                        {
                            shortestBurstTime = burstTimes[i];
                            shortestProcess = i;
                        }
                    }

                    if (shortestProcess != -1)
                    {
                        //execute the selected process
                        waitTimes[shortestProcess] = currentTime - arrivalTimes[shortestProcess];
                        currentTime += burstTimes[shortestProcess];
                        completionTimes[shortestProcess] = currentTime;
                        completed[shortestProcess] = true;
                        completedProcesses++;
                        totalTurnaroundTime += (completionTimes[shortestProcess] - arrivalTimes[shortestProcess]);

                        outputString += ("P" + (shortestProcess + 1) + " Burst/Waiting/Turnaround Time: "
                                         + burstTimes[shortestProcess] + "/" + waitTimes[shortestProcess] + "/"
                                         + (completionTimes[shortestProcess] - arrivalTimes[shortestProcess]) + "\r\n");
                    }
                    else
                    {
                        //increment the current time if no available process is found
                        currentTime++;
                    }
                }

                double totalWaitTime = 0.0;
                for (int i = 0; i < numProcesses; i++)
                {
                    totalWaitTime += waitTimes[i];
                }

                double averageWaitTime = totalWaitTime / numProcesses;
                outputString += "Average Waiting Time = " + averageWaitTime + "\r\n";
                double averageTurnaroundTime = totalTurnaroundTime / numProcesses;
                outputString += "Average Turnaround Time = " + averageTurnaroundTime + "\r\n";

                MessageBox.Show(outputString, "Process Information", MessageBoxButtons.OK);
            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int numProcesses = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTimes = new double[numProcesses];
                double[] waitTimes = new double[numProcesses];
                double[] arrivalTimes = new double[numProcesses];
                double[] completionTimes = new double[numProcesses];
                int[] priorities = new int[numProcesses];
                bool[] completed = new bool[numProcesses];
                string outputString = "";
                double totalTurnaroundTime = 0;

                //collect process information
                for (int i = 0; i < numProcesses; i++)
                {
                    string burstInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    string arrivalInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                           "Arrival time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    string priorityInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                           "Priority for P" + (i + 1),
                                                           "",
                                                           -1, -1);

                    burstTimes[i] = Convert.ToDouble(burstInput);
                    arrivalTimes[i] = Convert.ToDouble(arrivalInput);
                    priorities[i] = Convert.ToInt32(priorityInput);
                    completed[i] = false;
                }

                double currentTime = 0;
                int completedProcesses = 0;

                //run until all processes are completed
                while (completedProcesses < numProcesses)
                {
                    int highestPriorityProcess = -1;
                    int highestPriority = int.MaxValue;

                    //find the process with the highest priority that has arrived
                    for (int i = 0; i < numProcesses; i++)
                    {
                        if (!completed[i] && arrivalTimes[i] <= currentTime && priorities[i] < highestPriority)
                        {
                            highestPriority = priorities[i];
                            highestPriorityProcess = i;
                        }
                    }

                    if (highestPriorityProcess != -1)
                    {
                        //execute the selected process
                        waitTimes[highestPriorityProcess] = currentTime - arrivalTimes[highestPriorityProcess];
                        currentTime += burstTimes[highestPriorityProcess];
                        completionTimes[highestPriorityProcess] = currentTime;
                        completed[highestPriorityProcess] = true;
                        completedProcesses++;
                        totalTurnaroundTime += (completionTimes[highestPriorityProcess] - arrivalTimes[highestPriorityProcess]);

                        outputString += ("P" + (highestPriorityProcess + 1) + " Burst/Waiting/Turnaround Time: "
                                         + burstTimes[highestPriorityProcess] + "/" + waitTimes[highestPriorityProcess] + "/"
                                         + (completionTimes[highestPriorityProcess] - arrivalTimes[highestPriorityProcess]) + "\r\n");
                    }
                    else
                    {
                        //increment the current time if no available process is found
                        currentTime++;
                    }
                }

                double totalWaitTime = 0.0;
                for (int i = 0; i < numProcesses; i++)
                {
                    totalWaitTime += waitTimes[i];
                }

                double averageWaitTime = totalWaitTime / numProcesses;
                outputString += "Average Waiting Time = " + averageWaitTime + "\r\n";
                double averageTurnaroundTime = totalTurnaroundTime / numProcesses;
                outputString += "Average Turnaround Time = " + averageTurnaroundTime + "\r\n";

                MessageBox.Show(outputString, "Process Information", MessageBoxButtons.OK);
            }
            else
            {
                //this.Hide();
            }
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int numProcesses = Convert.ToInt16(userInput);
            double timeQuantum = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox("Enter the time quantum: ", "Time Quantum", "", -1, -1));

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTimes = new double[numProcesses];
                double[] remainingTimes = new double[numProcesses];
                double[] waitTimes = new double[numProcesses];
                double[] arrivalTimes = new double[numProcesses];
                double[] completionTimes = new double[numProcesses];
                bool[] completed = new bool[numProcesses];
                string outputString = "";
                double totalTurnaroundTime = 0;

                //collect process spec information
                for (int i = 0; i < numProcesses; i++)
                {
                    string burstInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    string arrivalInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                           "Arrival time for P" + (i + 1),
                                                           "",
                                                           -1, -1);

                    burstTimes[i] = Convert.ToDouble(burstInput);
                    remainingTimes[i] = burstTimes[i];
                    arrivalTimes[i] = Convert.ToDouble(arrivalInput);
                    completed[i] = false;
                }

                double currentTime = 0;
                int completedProcesses = 0;
                Queue<int> processQueue = new Queue<int>();

                //initially add processes that have arrived by time 0 to the queue
                for (int i = 0; i < numProcesses; i++)
                {
                    if (arrivalTimes[i] <= currentTime)
                    {
                        processQueue.Enqueue(i);
                    }
                }

                while (completedProcesses < numProcesses)
                {
                    if (processQueue.Count > 0)
                    {
                        int currentProcess = processQueue.Dequeue();

                        //calculate the time slice for this process
                        double timeSlice = Math.Min(timeQuantum, remainingTimes[currentProcess]);

                        //simulate execution by time slice
                        remainingTimes[currentProcess] -= timeSlice;
                        currentTime += timeSlice;

                        //check if the process is completed
                        if (remainingTimes[currentProcess] == 0)
                        {
                            completed[currentProcess] = true;
                            completedProcesses++;
                            completionTimes[currentProcess] = currentTime;
                            totalTurnaroundTime += (completionTimes[currentProcess] - arrivalTimes[currentProcess]);

                            outputString += ("P" + (currentProcess + 1) + " Burst/Waiting/Turnaround Time: "
                                             + burstTimes[currentProcess] + "/" + waitTimes[currentProcess] + "/"
                                             + (completionTimes[currentProcess] - arrivalTimes[currentProcess]) + "\r\n");
                        }
                        else
                        {
                            //re-enqueue the process if it's not finished
                            processQueue.Enqueue(currentProcess);
                        }

                        //update waiting times for processes that arrived during the current time slice
                        for (int i = 0; i < numProcesses; i++)
                        {
                            if (i != currentProcess && !completed[i] && arrivalTimes[i] <= currentTime && !processQueue.Contains(i))
                            {
                                processQueue.Enqueue(i);
                            }
                        }
                    }
                    else
                    {
                        //increment current time if no process is available
                        currentTime++;
                    }

                    //update waiting times for processes in the queue
                    foreach (int procIndex in processQueue)
                    {
                        if (procIndex != processQueue.Peek())
                        {
                            waitTimes[procIndex] += timeQuantum;
                        }
                    }
                }

                double totalWaitTime = 0.0;
                for (int i = 0; i < numProcesses; i++)
                {
                    totalWaitTime += waitTimes[i];
                }

                double averageWaitTime = totalWaitTime / numProcesses;
                outputString += "Average Waiting Time = " + averageWaitTime + "\r\n";
                double averageTurnaroundTime = totalTurnaroundTime / numProcesses;
                outputString += "Average Turnaround Time = " + averageTurnaroundTime + "\r\n";

                MessageBox.Show(outputString, "Process Information", MessageBoxButtons.OK);
            }
            else
            {
                //this.Hide();
            }
        }

        public static void strfAlgorithm(string userInput)
        {
            int numProcesses = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Shortest Time Remaining First Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTimes = new double[numProcesses];
                double[] remainingTimes = new double[numProcesses];
                double[] waitTimes = new double[numProcesses];
                double[] arrivalTimes = new double[numProcesses];
                double[] completionTimes = new double[numProcesses];
                bool[] completed = new bool[numProcesses];
                string outputString = "";
                double totalTurnaroundTime = 0;

                //collect process spec information
                for (int i = 0; i < numProcesses; i++)
                {
                    string burstInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    string arrivalInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                           "Arrival time for P" + (i + 1),
                                                           "",
                                                           -1, -1);

                    burstTimes[i] = Convert.ToDouble(burstInput);
                    remainingTimes[i] = burstTimes[i];
                    arrivalTimes[i] = Convert.ToDouble(arrivalInput);
                    completed[i] = false;
                }

                double currentTime = 0;
                int completedProcesses = 0;

                //run until all specified processes are completed
                while (completedProcesses < numProcesses)
                {
                    double shortestTimeRemaining = double.MaxValue;
                    int currentProcess = -1;

                    for (int i = 0; i < numProcesses; i++)
                    {
                        //consider processes that have arrived and are not completed
                        if (!completed[i] && arrivalTimes[i] <= currentTime && remainingTimes[i] < shortestTimeRemaining)
                        {
                            shortestTimeRemaining = remainingTimes[i];
                            currentProcess = i;
                        }
                    }

                    if (currentProcess != -1)
                    {
                        //simulate execution by one time unit
                        remainingTimes[currentProcess]--;
                        currentTime++;

                        //check if the process is completed
                        if (remainingTimes[currentProcess] == 0)
                        {
                            completed[currentProcess] = true;
                            completedProcesses++;
                            completionTimes[currentProcess] = currentTime;
                            totalTurnaroundTime += (completionTimes[currentProcess] - arrivalTimes[currentProcess]);

                            outputString += ("P" + (currentProcess + 1) + " Burst/Waiting/Turnaround Time: "
                                             + burstTimes[currentProcess] + "/" + waitTimes[currentProcess] + "/"
                                             + (completionTimes[currentProcess] - arrivalTimes[currentProcess]) + "\r\n");
                        }

                        //update waiting times for other processes
                        for (int i = 0; i < numProcesses; i++)
                        {
                            if (i != currentProcess && !completed[i] && arrivalTimes[i] <= currentTime)
                            {
                                waitTimes[i]++;
                            }
                        }
                    }
                    else
                    {
                        //if no process is available to execute, simply increment the current time
                        currentTime++;
                    }
                }

                double totalWaitTime = 0.0;
                for (int i = 0; i < numProcesses; i++)
                {
                    totalWaitTime += waitTimes[i];
                }

                double averageWaitTime = totalWaitTime / numProcesses;
                outputString += "Average Waiting Time = " + averageWaitTime + "\r\n";
                double averageTurnaroundTime = totalTurnaroundTime / numProcesses;
                outputString += "Average Turnaround Time = " + averageTurnaroundTime + "\r\n";

                MessageBox.Show(outputString, "Process Information", MessageBoxButtons.OK);
            }
            else
            {
                //this.Hide();
            }
        }

        public static void hrrnAlgorithm(string userInput)
        {
            int numProcesses = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Highest Response Ratio Next Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTimes = new double[numProcesses];
                double[] waitTimes = new double[numProcesses];
                double[] arrivalTimes = new double[numProcesses];
                double[] completionTimes = new double[numProcesses];
                bool[] completed = new bool[numProcesses];
                string outputString = "";
                double totalTurnaroundTime = 0;

                // Collect process information
                for (int i = 0; i < numProcesses; i++)
                {
                    string burstInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    string arrivalInput =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                           "Arrival time for P" + (i + 1),
                                                           "",
                                                           -1, -1);

                    burstTimes[i] = Convert.ToDouble(burstInput);
                    arrivalTimes[i] = Convert.ToDouble(arrivalInput);
                    completed[i] = false;
                }

                double currentTime = 0;
                int completedProcesses = 0;

                // Run until all processes are completed
                while (completedProcesses < numProcesses)
                {
                    double highestResponseRatio = -1;
                    int nextProcess = -1;

                    // Find the process with the highest response ratio
                    for (int i = 0; i < numProcesses; i++)
                    {
                        if (!completed[i] && arrivalTimes[i] <= currentTime)
                        {
                            double waitingTime = currentTime - arrivalTimes[i];
                            double responseRatio = (waitingTime + burstTimes[i]) / burstTimes[i];

                            if (responseRatio > highestResponseRatio)
                            {
                                highestResponseRatio = responseRatio;
                                nextProcess = i;
                            }
                        }
                    }

                    if (nextProcess != -1)
                    {
                        // Execute the selected process
                        waitTimes[nextProcess] = currentTime - arrivalTimes[nextProcess];
                        currentTime += burstTimes[nextProcess];
                        completionTimes[nextProcess] = currentTime;
                        completed[nextProcess] = true;
                        completedProcesses++;
                        totalTurnaroundTime += (completionTimes[nextProcess] - arrivalTimes[nextProcess]);

                        outputString += ("P" + (nextProcess + 1) + " Burst/Waiting/Turnaround Time: "
                                         + burstTimes[nextProcess] + "/" + waitTimes[nextProcess] + "/"
                                         + (completionTimes[nextProcess] - arrivalTimes[nextProcess]) + "\r\n");
                    }
                    else
                    {
                        // Increment the current time if no available process is found
                        currentTime++;
                    }
                }

                double totalWaitTime = 0.0;
                for (int i = 0; i < numProcesses; i++)
                {
                    totalWaitTime += waitTimes[i];
                }

                double averageWaitTime = totalWaitTime / numProcesses;
                outputString += "Average Waiting Time = " + averageWaitTime + "\r\n";
                double averageTurnaroundTime = totalTurnaroundTime / numProcesses;
                outputString += "Average Turnaround Time = " + averageTurnaroundTime + "\r\n";

                MessageBox.Show(outputString, "Process Information", MessageBoxButtons.OK);
            }
        }
    }
}

