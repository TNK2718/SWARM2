using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm {
    // Chromosomeの集合
    public abstract class ChromosomesBase<T> {
        protected List<Chromosome<T>> chromosomes;
        public abstract void SetUp(int chromosomeMaxNumber);
        public abstract void SetChromosome(int individual, int index, T value);
        public abstract void SetScore(int individual, double _score);
        public abstract void AddChromosome(T[] value, double score);
        public abstract void RemoveChromosome(int individual);
        public abstract T ReadChromosome(int individual, int index);
        public abstract double ReadScore(int individual);
    }
}
