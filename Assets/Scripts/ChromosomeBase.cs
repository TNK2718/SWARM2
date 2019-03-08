using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

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

public class Chromosome<T> {
    public Chromosome(int chromosomesize) {
        value = new T[chromosomesize];
        score = 0;
    }
    public T[] value;
    public double score;
}