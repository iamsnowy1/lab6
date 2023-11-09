using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    public delegate void TaskExecution<TTask>(TTask task);

    public class TaskScheduler<TTask, TPriority> where TPriority : IComparable<TPriority>
    {
        private readonly PriorityQueue<TTask, TPriority> taskQueue = new PriorityQueue<TTask, TPriority>();
        private readonly Func<TTask> initializeTask;
        private readonly Action<TTask> resetTask;

        public TaskScheduler(Func<TTask> initializeTask, Action<TTask> resetTask)
        {
            this.initializeTask = initializeTask ?? throw new ArgumentNullException(nameof(initializeTask));
            this.resetTask = resetTask ?? throw new ArgumentNullException(nameof(resetTask));
        }

        public void AddTask(TTask task, TPriority priority)
        {
            taskQueue.Enqueue(task, priority);
        }

        public void ExecuteNext(TaskExecution<TTask> executeTask)
        {
            if (taskQueue.Count > 0)
            {
                TTask task = taskQueue.Dequeue();
                executeTask(task);
                resetTask(task); 
            }
            else
            {
                Console.WriteLine("No tasks in the queue.");
            }
        }

        public TTask GetTaskFromPool()
        {
            return initializeTask();
        }

        public void ReturnTaskToPool(TTask task)
        {
            resetTask(task);
        }
    }

    
    public class PriorityQueue<TItem, TPriority> where TPriority : IComparable<TPriority>
    {
        private readonly List<Tuple<TItem, TPriority>> elements = new List<Tuple<TItem, TPriority>>();
        public int Count => elements.Count;
        public void Enqueue(TItem item, TPriority priority)
        {
            elements.Add(new Tuple<TItem, TPriority>(item, priority));
            elements.Sort((x, y) => y.Item2.CompareTo(x.Item2));
        }

        public TItem Dequeue()
        {
            if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");
            TItem item = elements[0].Item1;
            elements.RemoveAt(0);
            return item;
        }
    }

    class Program
    {
        static void Main()
        {
            
            TaskScheduler<string, int> scheduler = new TaskScheduler<string, int>(
                initializeTask: () => "New Task",
                resetTask: task => Console.WriteLine($"Task '{task}' has been reset.")
            );

            scheduler.AddTask("Task1", 3);
            scheduler.AddTask("Task2", 1);
            scheduler.AddTask("Task3", 2);
            TaskExecution<string> executeTask = task => Console.WriteLine($"Executing task: {task}");
            scheduler.ExecuteNext(executeTask);
            scheduler.ExecuteNext(executeTask);
            scheduler.ExecuteNext(executeTask);
            string taskFromPool = scheduler.GetTaskFromPool();
            Console.WriteLine($"Task from pool: {taskFromPool}");
            scheduler.ReturnTaskToPool(taskFromPool);
        }
    }
}
