using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueueWithPrioritete
{
    public partial class Form1: Form
    {
        private int[] Heap;
        private int size;
        private const int CAPACITY = 15;
        private Random random;

        public Form1()
        {
            InitializeComponent();

            ArrayDataGridView.ColumnHeadersVisible = false;
            ArrayDataGridView.RowHeadersVisible = false;
            ArrayDataGridView.RowCount = 1;
            ArrayDataGridView.ColumnCount = 15;
            ArrayDataGridView.Rows[0].Height = 45;

            for (int i = 0; i < ArrayDataGridView.ColumnCount; i++)
            {
                ArrayDataGridView.Columns[i].Width = 55;
            }

            HeapDataGridView.ColumnHeadersVisible = false;
            HeapDataGridView.RowHeadersVisible = false;
            HeapDataGridView.RowCount = 4;
            HeapDataGridView.ColumnCount = 15;

            for (int i = 0; i < HeapDataGridView.RowCount; i++)
            {
                HeapDataGridView.Rows[i].Height = 41;
            }


            for (int i = 0; i < HeapDataGridView.ColumnCount; i++)
            {
                HeapDataGridView.Columns[i].Width = 55;
            }

            ChooseResultdataGridView.ColumnHeadersVisible = false;
            ChooseResultdataGridView.RowHeadersVisible = false;
            ChooseResultdataGridView.RowCount = 1;
            ChooseResultdataGridView.ColumnCount = 15;
            ChooseResultdataGridView.Rows[0].Height = 45;

            for (int i = 0; i < ChooseResultdataGridView.ColumnCount; i++)
            {
                ChooseResultdataGridView.Columns[i].Width = 55;
            }

            Heap = new int[CAPACITY];
            size = 0;
            random = new Random();

            for (int i = 0; i < CAPACITY; ++i)
            {
                Heap[i] = 0;
            }
        }

        private int ParentIndex(int i) => (i - 1) / 2;
        private int LeftChildIndex(int i) => 2 * i + 1;
        private int RightChildIndex(int i) => 2 * i + 2;

        private void Up(int index)
        {

            while (index > 0 && Heap[ParentIndex(index)] < Heap[index])
            {
                int parent = ParentIndex(index);
                (Heap[index], Heap[parent]) = (Heap[parent], Heap[index]);
                index = parent;
            }

        }
        
        private void Down(int index)
        {

            while (LeftChildIndex(index) < size)
            {
                int left = LeftChildIndex(index);
                int right = RightChildIndex(index);
                int largest = left;

                if (right < size && Heap[right] > Heap[left])
                {
                    largest = right;
                }

                if (Heap[index] >= Heap[largest]) break;


                (Heap[index], Heap[largest]) = (Heap[largest], Heap[index]);

                index = largest;
            }
        }
        
        private void Insert(int value)
        {
            if (size >= CAPACITY)
            {
                MessageBox.Show("Очередь переполнена!");
                return;
            }

            Heap[size] = value;
            Up(size);
            size++;
        }

        private void CreateRandQueue()
        {
            size = 0;

            for (int i = 0; i < CAPACITY; ++i)
            {
                int value = random.Next(10, 100);
                Insert(value);
            }
        }

        public void ClearQueue()
        {
            for (int i = 0; i < size; i++)
                Heap[i] = 0;
            size = 0;
        }

        public void printArray(int[] a)
        {
            for (int i = 0; i < CAPACITY; i++)
            {
                ArrayDataGridView.Rows[0].Cells[i].Value = Heap[i];
            }
        }
        public void printTree(int[] a)
        {
            HeapDataGridView.Rows[0].Cells[7].Value = a[0];
            HeapDataGridView.Rows[1].Cells[3].Value = a[1];
            HeapDataGridView.Rows[1].Cells[11].Value = a[2];
            HeapDataGridView.Rows[2].Cells[1].Value = a[3];
            HeapDataGridView.Rows[2].Cells[5].Value = a[4];
            HeapDataGridView.Rows[2].Cells[9].Value = a[5]; 
            HeapDataGridView.Rows[2].Cells[13].Value = a[6];
            HeapDataGridView.Rows[3].Cells[0].Value = a[7];
            HeapDataGridView.Rows[3].Cells[2].Value = a[8]; 
            HeapDataGridView.Rows[3].Cells[4].Value = a[9];
            HeapDataGridView.Rows[3].Cells[6].Value = a[10];
            HeapDataGridView.Rows[3].Cells[8].Value = a[11];
            HeapDataGridView.Rows[3].Cells[10].Value = a[12];
            HeapDataGridView.Rows[3].Cells[12].Value = a[13];
            HeapDataGridView.Rows[3].Cells[14].Value = a[14];
        }

        public void Clear_Tab()
        {
            for (int i = 0; i < ArrayDataGridView.ColumnCount; i++)
            {
                ArrayDataGridView.Rows[0].Cells[i].Value = "";
            }

            for (int i = 0; i < HeapDataGridView.RowCount; i++)
            {
                for (int j = 0; j < HeapDataGridView.ColumnCount; j++)
                {
                    HeapDataGridView.Rows[i].Cells[j].Value = "";
                }
            }

            for (int i = 0; i < ChooseResultdataGridView.ColumnCount; i++)
            {
                ChooseResultdataGridView.Rows[0].Cells[i].Value = "";
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateQueueBtn_Click(object sender, EventArgs e)
        {
            CreateRandQueue();
            printArray(Heap);
            printTree(Heap);
        }

        private void ClearQueueBtn_Click(object sender, EventArgs e)
        {
            ClearQueue();
            Clear_Tab();
        }

        private void GetLargestBtn_Click(object sender, EventArgs e)
        {
            int max = ExtractMax();
            if (max == -1) return;

            bool placed = false;
            for (int i = 0; i < ChooseResultdataGridView.ColumnCount; i++)
            {
                if (ChooseResultdataGridView.Rows[0].Cells[i].Value == null ||
                    ChooseResultdataGridView.Rows[0].Cells[i].Value.ToString() == "")
                {
                    ChooseResultdataGridView.Rows[0].Cells[i].Value = max;
                    placed = true;
                    break;
                }
            }

            if (!placed)
            {
                MessageBox.Show("Массив результата выборок заполнен!");
            }

            printArray(Heap);
            printTree(Heap);
        }
        
        private int ExtractMax()
        {
            if (size == 0)
            {
                MessageBox.Show("Очередь пуста!");
                return -1;
            }

            int max = Heap[0];
            Heap[0] = Heap[size - 1];
            Heap[size - 1] = 0;
            size--;
            Down(0);
            return max;
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            int value = (int) NewElemDropDown.Value;
            Insert(value);
            printArray(Heap);
            printTree(Heap);
        }

        private void ChangePrioriteteBtn_Click(object sender, EventArgs e)
        {
            int removedValue = (int) PrioriteteFromDropDown.Value;
            int addedValue = (int) PrioriteteToDropDown.Value;
            int indexRemovedValue = 0;

            bool found = false;
            for (int i = 0; i < size; i++)
            {
                if (Heap[i] == removedValue) 
                {
                    Heap[i] = addedValue;
                    indexRemovedValue = i;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Элемент не найден! ");
                return;
            }

            if (removedValue > addedValue)
            {
                Down(indexRemovedValue);
            }

            else { 
                Up(indexRemovedValue); 
            }

            printArray(Heap);
            printTree(Heap);
        }
    }
}
