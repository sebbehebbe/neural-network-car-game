﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;

public class NeuralNetwork {

	// Options
	private int[] network;
	private int population = 50;
	private double elitsm = 0.2;
	private double randomBehaviour = 0.2;
	private double mutationRate = 0.1;
	private double mutationRange = 0.5;
	private int scoreSort = -1;
	private int nbChild = 1;

	static System.Random rnd = new System.Random ();

	// Network
	private List<Layer> layers = new List<Layer>();


	public NeuralNetwork(){
		network = new int[]{ 1, 1, 1 };
	}

	private double activation(double x) {
		return (1 / (Math.Exp(x) + 1));
	}

	private static double randomClamped() {
		return rnd.NextDouble () * 2 - 1;
	}

	public void perceptronGeneration(int input, int[] hiddens, int output) {
		int index = 0;
		int previousNeurons = 0;
		Layer layer = new Layer ();

		layer.populate (input, previousNeurons);

		previousNeurons = input;

		layers.Add (layer);
		index++;

		for (int i = 0; i < hiddens.Length; i++) {
			Layer nextLayer = new Layer(index);

			nextLayer.populate (hiddens [i], previousNeurons);
			previousNeurons = hiddens [i];
			layers.Add (nextLayer);
			index++;
		}

		// Output layer
		Layer outputLayer = new Layer (index);
		outputLayer.populate (output, previousNeurons);

		layers.Add(outputLayer);

	}

	private int[] getNumberOfNeuronsPerLayer() {
		int[] nbNeuronsPerLayer = new int[layers.Count];

		for (int i = 0; i < layers.Count; i++) {
			Layer layer = layers [i];
			nbNeuronsPerLayer [i] = layer.getNeurons ().Length;
		}

		return nbNeuronsPerLayer;
	}

	public double[][][] getWeights() {

		double[][][] weights = new double[][][]{};

		for (int i = 0; i < layers.Count; i++) {
			Layer layer = layers [i];
			Neuron[] layerNeurons = layer.getNeurons();
			for (int j = 0; j < layerNeurons.Length; j++) {
				weights [i] [j] = layerNeurons [j].getWeights ();
			}
		}

		return weights;
	}

	public List<Layer> getLayers() {
		return layers;
	}
		
	public class Neuron {
		private int value = 0;
		private double[] weights;

		public void populate(int nb){
			weights = new double[nb];
			for (int i = 0; i < nb; i++) {
				weights [i] = NeuralNetwork.randomClamped ();
			}
		}

		public double[] getWeights() {
			return weights;
		}

	}

	public class Layer {
		private int id;
		private Neuron[] neurons = new Neuron[] {};

		public Layer() {
			id = 0;
		}

		public Layer(int index) {
			id = index;
		}

		public void populate(int nbNeurons, int nbInputs) {
			neurons = new Neuron[nbNeurons];
			for (int i = 0; i < nbNeurons; i++) {
				Neuron n = new Neuron ();
				n.populate (nbInputs);
				this.neurons [i] = n;
			}
		}

		public Neuron[] getNeurons() {
			return neurons;
		}
	}




}
