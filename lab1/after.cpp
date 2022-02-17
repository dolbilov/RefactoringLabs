#include <iostream>
#include <cstdlib>
#include <ctime>
#include <climits>
#include <iomanip>

using namespace std;


/*
 Function that input integer with correct checking
 */
int InputInt(const string& comment = "", const int min = INT_MIN, const int max = INT_MAX) {
	if (!comment.empty()) {
		cout << comment << endl;
	}

	int temp;
	cin >> temp;
	while (cin.fail() || cin.peek()!='\n' || temp < min || temp > max) {
		cin.clear();
		cin.ignore(cin.rdbuf()->in_avail(), '\n');
		cout << "Input error. Entered number should be between " << min << " and " << max << endl;
		cin >> temp;
	}

	return temp;
}

class Matrix {
public:
	int rowsCount;
	int columnsCount;
	int** data;

	Matrix() {
		rowsCount = InputInt("Введите количество строк матрицы", 1);
		columnsCount = InputInt("Введите количество столбцов матрицы", 1);
		CreateMatrix();
		
	}

	Matrix(const int _rowsCount, const int _columnsCount) {
		rowsCount = _rowsCount;
		columnsCount = _columnsCount;
		CreateMatrix();
	}

	Matrix(const int _rowsCount, const int _columnsCount, const Matrix& source) : Matrix(_rowsCount, _columnsCount) {
		for (int i = 0; i < source.rowsCount; i++) {
			for (int j = 0; j < source.columnsCount; j++) {
				data[i][j] = source.data[i][j];
			}
		}
	}

	~Matrix() {
		for (int i = 0; i < rowsCount; i++) {
			delete[] data[i];
		}

		delete[] data;
	}

	void Print(const string& comment = "") {
		cout << endl;

		if (!comment.empty())
			cout << comment << endl;

		for (int i = 0; i < rowsCount; i++) {
			for (int j = 0; j < columnsCount; j++) {
				cout << setw(4) << data[i][j];
			}
			cout << endl;
		}

		cout << endl;
	}

	void FillFromConsole() {
		for (int i = 0; i < rowsCount; i++) {
			for (int j = 0; j < columnsCount; j++) {
				data[i][j] = InputInt();
			}
		}
	}

	void FillRandomly() {
		for (int i = 0; i < rowsCount; i++) {
			for (int j = 0; j < columnsCount; j++) {
				data[i][j] = rand() % 9;
			}
		}
	}

private:
	void CreateMatrix() {
		data = new int* [rowsCount];
		for (int i = 0; i < rowsCount; i++) {
			data[i] = new int[columnsCount];
			for (int j = 0; j < columnsCount; j++) {
				data[i][j] = 0;
			}
		}
	}
};

int DefineMaxDimensionSize(const Matrix& matrix1, const Matrix& matrix2) {
	int tempSize = 2;
	while (tempSize < matrix1.rowsCount || tempSize < matrix1.columnsCount || tempSize < matrix2.rowsCount || tempSize < matrix2.columnsCount) {
		tempSize *= 2;
	}
	return tempSize;
}

void MultiplyMatrices(const Matrix& matrix1, const Matrix& matrix2) {
	///////////////////////////////////////////////////////////////////////////////
	/////////////////Приведение матриц к требуемому размеру////////////////////////
	///////////////////////////////////////////////////////////////////////////////

	int maxSize = DefineMaxDimensionSize(matrix1, matrix2);

	Matrix reducedMatrix1 = Matrix(maxSize, maxSize, matrix1);
	Matrix reducedMatrix2 = Matrix(maxSize, maxSize, matrix2);

	cout << "Приведенные матрицы\n";
	reducedMatrix1.Print("Матрица 1");
	reducedMatrix2.Print("Матрица 2");

	///////////////////////////////////////////////////////////////////////////////
	///////////////Разбиение матриц на подматрицы и их заполнение//////////////////
	///////////////////////////////////////////////////////////////////////////////

	Matrix mat1 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat2 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat3 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat4 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat5 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat6 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat7 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat8 = Matrix(maxSize / 2, maxSize / 2);


	for (int i = 0; i < maxSize / 2; i++) {
		for (int j = 0; j < maxSize / 2; j++) {
			mat1.data[i][j] = reducedMatrix1.data[i][j];
			mat2.data[i][j] = reducedMatrix1.data[i][j + maxSize / 2];
			mat3.data[i][j] = reducedMatrix1.data[i + maxSize / 2][j];
			mat4.data[i][j] = reducedMatrix1.data[i + maxSize / 2][j + maxSize / 2];
			mat5.data[i][j] = reducedMatrix2.data[i][j];
			mat6.data[i][j] = reducedMatrix2.data[i][j + maxSize / 2];
			mat7.data[i][j] = reducedMatrix2.data[i + maxSize / 2][j];
			mat8.data[i][j] = reducedMatrix2.data[i + maxSize / 2][j + maxSize / 2];
		}

	}

	///////////////////////////////////////////////////////////////////////////////
	////////////////////////Создание промежуточных матриц//////////////////////////
	///////////////////////////////////////////////////////////////////////////////

	Matrix tempMatrix1 = Matrix(maxSize / 2, maxSize / 2);
	Matrix tempMatrix2 = Matrix(maxSize / 2, maxSize / 2);
	Matrix tempMatrix3 = Matrix(maxSize / 2, maxSize / 2);
	Matrix tempMatrix4 = Matrix(maxSize / 2, maxSize / 2);
	Matrix tempMatrix5 = Matrix(maxSize / 2, maxSize / 2);
	Matrix tempMatrix6 = Matrix(maxSize / 2, maxSize / 2);
	Matrix tempMatrix7 = Matrix(maxSize / 2, maxSize / 2);



	///////////////////////////////////////////////////////////////////////////////
	////////////////////Вычисление значений промежуточных матриц///////////////////
	///////////////////////////////////////////////////////////////////////////////

	for (int i = 0; i < maxSize / 2; i++) {
		for (int j = 0; j < maxSize / 2; j++) {
			tempMatrix1.data[i][j] = 0;
			tempMatrix2.data[i][j] = 0;
			tempMatrix3.data[i][j] = 0;
			tempMatrix4.data[i][j] = 0;
			tempMatrix5.data[i][j] = 0;
			tempMatrix6.data[i][j] = 0;
			tempMatrix7.data[i][j] = 0;

			for (int z = 0; z < maxSize / 2; z++) {
				tempMatrix1.data[i][j] += (mat1.data[i][z] + mat4.data[i][z]) * (mat5.data[z][j] + mat8.data[z][j]);
				tempMatrix2.data[i][j] += (mat3.data[i][z] + mat4.data[i][z]) * mat5.data[z][j];
				tempMatrix3.data[i][j] += mat1.data[i][z] * (mat6.data[z][j] - mat8.data[z][j]);
				tempMatrix4.data[i][j] += mat4.data[i][z] * (mat7.data[z][j] - mat5.data[z][j]);
				tempMatrix5.data[i][j] += (mat1.data[i][z] + mat2.data[i][z]) * mat8.data[z][j];
				tempMatrix6.data[i][j] += (mat3.data[i][z] - mat1.data[i][z]) * (mat5.data[z][j] + mat6.data[z][j]);
				tempMatrix7.data[i][j] += (mat2.data[i][z] - mat4.data[i][z]) * (mat7.data[z][j] + mat8.data[z][j]);
			}
		}
	}

	///////////////////////////////////////////////////////////////////////////////
	///////////////////////Создание вспомогательных матриц/////////////////////////
	///////////////////////////////////////////////////////////////////////////////

	Matrix mat9 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat10 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat11 = Matrix(maxSize / 2, maxSize / 2);
	Matrix mat12 = Matrix(maxSize / 2, maxSize / 2);



	///////////////////////////////////////////////////////////////////////////////
	////////////Подсчет значений вспомогательных матриц из промежуточных///////////
	///////////////////////////////////////////////////////////////////////////////

	for (int i = 0; i < maxSize / 2; i++) {
		for (int j = 0; j < maxSize / 2; j++) {
			mat9.data[i][j] = tempMatrix1.data[i][j] + tempMatrix4.data[i][j] - tempMatrix5.data[i][j] + tempMatrix7.data[i][j];
			mat10.data[i][j] = tempMatrix3.data[i][j] + tempMatrix5.data[i][j];
			mat11.data[i][j] = tempMatrix2.data[i][j] + tempMatrix4.data[i][j];
			mat12.data[i][j] = tempMatrix1.data[i][j] - tempMatrix2.data[i][j] + tempMatrix3.data[i][j] + tempMatrix6.data[i][j];
		}
	}

	///////////////////////////////////////////////////////////////////////////////
	///////////////////Создание результирующей матрицы/////////////////////////////
	///////////////////////////////////////////////////////////////////////////////

	Matrix res = Matrix(maxSize, maxSize);

	///////////////////////////////////////////////////////////////////////////////
	///////Занесение информации из вспомогательных матриц в результирующую/////////
	///////////////////////////////////////////////////////////////////////////////

	for (int i = 0; i < maxSize / 2; i++) {
		for (int j = 0; j < maxSize / 2; j++) {
			res.data[i][j] = mat9.data[i][j];
			res.data[i][j + maxSize / 2] = mat10.data[i][j];
			res.data[i + maxSize / 2][j] = mat11.data[i][j];
			res.data[i + maxSize / 2][j + maxSize / 2] = mat12.data[i][j];
		}
	}

	///////////////////////////////////////////////////////////////////////////////
	////////////////Выравнивание границ результирующей матрицы/////////////////////
	///////////////////////////////////////////////////////////////////////////////

	int x = 0, y = 0, f = 100, s = 100;
	for (int i = 0; i < maxSize; i++) {
		x = 0;
		y = 0;
		for (int j = 0; j < maxSize; j++) {
			if (res.data[i][j] != 0) {
				x++;
				f = 100;
			}
			if (res.data[j][i] != 0) {
				y++;
				s = 100;
			}
		}

		if (x == 0 && i < f) {
			f = i;
		}
		if (y == 0 && i < s) {
			s = i;
		}
	}


	Matrix reducedRes = Matrix(f, s);
	for (int i = 0; i < f; i++) {
		for (int j = 0; j < s; j++)
			reducedRes.data[i][j] = res.data[i][j];
	}

	reducedRes.Print("Результирующая матрица");
}


void main() {
	srand(time(NULL));
	system("chcp 1251");
	cout << "Вас приветствует программа быстрого перемножения матриц методом Штрассена\n\n";
	

	Matrix matrix1 = Matrix();
	Matrix matrix2 = Matrix();

	///////////////////////////////////////////////////////////////////////////////
	////////////////Выбор способа заполнения и заполнение матриц///////////////////
	///////////////////////////////////////////////////////////////////////////////

	const int fillType = InputInt("Выберите способ заполнения матриц\n1 - Вручную \n2 - Случайным образом\n", 1, 2);

	switch (fillType) {
	case 1:
		matrix1.FillFromConsole();
		matrix2.FillFromConsole();
		break;
	case 2:
		matrix1.FillRandomly();
		matrix2.FillRandomly();
		break;
	default: 
		throw exception("Wrong fill type");
	}

	matrix1.Print("Матрица 1");
	matrix2.Print("Матрица 2");

	MultiplyMatrices(matrix1, matrix2);
	

	system("pause");	
}