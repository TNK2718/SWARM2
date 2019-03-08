namespace GeneticAlgorithm {
    // １つの染色体を表すクラス
    public class Chromosome<T> {
        public Chromosome(int chromosomesize) {
            value = new T[chromosomesize];
            score = 0;
        }
        public T[] value;
        public double score;
    }
}
