package System;

/**
 * @version 1.0
 * @created 12-nov-2013 15:55:30
 */
public class Class1 extends Class2 implements Interface1 {

	public Class3 m_Class3;

	public Class1(){

	}

	public void finalize() throws Throwable {
		super.finalize();
	}

}